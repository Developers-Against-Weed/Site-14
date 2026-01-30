// Originally ported from Ephemeral Space: https://github.com/EphemeralSpace/ephemeral-space
//
// SPDX-License-Identifier: MIT-WIZARDS

using Robust.Shared.Network;
using Robust.Shared.Prototypes;
using Robust.Shared.Spawners;
using Content.Site14.Common.Viewcone.Events;
using System.Numerics;

namespace Content.Site14.Shared.Viewcone;

/// <summary>
///     API for spawning viewcone effects and making sure source gets set correctly +
///     it spawns in the correct pos and shit
/// </summary>
public sealed class ViewconeEffectSystem : EntitySystem
{
    [Dependency] private readonly INetManager _net = default!;
    [Dependency] private readonly SharedTransformSystem _xform = default!;

    private static readonly EntProtoId DefaultFootstepEffect = "ViewconeEffectFootstep";

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<SpawnViewconeEffectEvent>(OnSpawnEffectEvent);
    }

    /// <summary>
    ///     Spawns the given effect entity at the player source, and sets relevant variables
    /// </summary>
    /// <param name="source">The player that originated the effect, or the entity to spawn next to if a relevant player doesn't exist</param>
    /// <param name="effect">The prototype ID of an effect entity to spawn (see viewcone_effects.yml)</param>
    /// <param name="angleOverride">The local rotation to set the effect to, instead of the parent rotation.</param>
    public void SpawnEffect(EntityUid source, EntProtoId effect, Angle? angleOverride = null)
    {
        var ent = PredictedSpawnNextToOrDrop(effect, source);
        var viewconeEffect = EnsureComp<ViewconeOccludableComponent>(ent);
        viewconeEffect.Inverted = true;
        viewconeEffect.Source = source;
        Dirty(ent, viewconeEffect);

        // set rotation
        _xform.SetLocalRotation(ent, angleOverride ?? Transform(source).LocalRotation);

        // also ensure this in case somehow something without it gets here.
        EnsureComp<TimedDespawnComponent>(ent);
    }

    private void OnSpawnEffectEvent(ref SpawnViewconeEffectEvent args)
    {
        if (!TerminatingOrDeleted(args.Uid))
            SpawnEffect(args.Uid, DefaultFootstepEffect, args.Direction);
    }

    /// <summary>
    ///     Checks if a target entity is within the viewcone of a viewer entity.
    /// </summary>
    /// <param name="viewer">The entity with the viewcone.</param>
    /// <param name="target">The entity to check visibility for.</param>
    /// <returns>True if the target is within the viewer's viewcone, false otherwise.</returns>
    public bool IsInViewcone(EntityUid viewer, EntityUid target)
    {
        if (!TryComp<ViewconeComponent>(viewer, out var viewcone))
            return true;

        var viewerXform = Transform(viewer);
        var targetXform = Transform(target);

        if (viewerXform.MapID != targetXform.MapID)
            return false;

        var viewerPos = _xform.GetWorldPosition(viewerXform);
        var targetPos = _xform.GetWorldPosition(targetXform);
        var delta = targetPos - viewerPos;
        var distance = delta.Length();

        if (distance <= viewcone.ConeIgnoreRadius)
            return true;

        var angleToTarget = new Angle(MathF.Atan2(delta.Y, delta.X));

        var viewAngle = _net.IsClient
            ? viewcone.DesiredViewAngle ?? viewcone.ViewAngle
            : _xform.GetWorldRotation(viewerXform) - Angle.FromDegrees(90);

        var angleDiff = Angle.ShortestDistance(viewAngle, angleToTarget);

        var halfCone = Angle.FromDegrees(viewcone.ConeAngle / 2f);

        return Math.Abs(angleDiff.Theta) <= halfCone.Theta;
    }

    /// <summary>
    ///     Checks if a world position is within the viewcone of a viewer entity.
    /// </summary>
    /// <param name="viewer">The entity with the viewcone.</param>
    /// <param name="targetPos">The world position to check.</param>
    /// <returns>True if the position is within the viewer's viewcone, false otherwise.</returns>
    public bool IsInViewcone(EntityUid viewer, Vector2 targetPos)
    {
        if (!TryComp<ViewconeComponent>(viewer, out var viewcone))
            return true;

        var viewerXform = Transform(viewer);
        var viewerPos = _xform.GetWorldPosition(viewerXform);
        var delta = targetPos - viewerPos;
        var distance = delta.Length();

        if (distance <= viewcone.ConeIgnoreRadius)
            return true;

        var angleToTarget = new Angle(MathF.Atan2(delta.Y, delta.X));

        var viewAngle = _net.IsClient
            ? viewcone.DesiredViewAngle ?? viewcone.ViewAngle
            : _xform.GetWorldRotation(viewerXform) - Angle.FromDegrees(90);

        var angleDiff = Angle.ShortestDistance(viewAngle, angleToTarget);

        var halfCone = Angle.FromDegrees(viewcone.ConeAngle / 2f);

        return Math.Abs(angleDiff.Theta) <= halfCone.Theta;
    }
}
