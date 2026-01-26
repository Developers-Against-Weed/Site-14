// SPDX-FileCopyrightText: 2026 Site-14 Contributors
//
// SPDX-License-Identifier: MPL-2.0
//
// Additional Use Restrictions apply:
// See /LICENSES/SITE14-ADDENDUM.md

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Content.Server.NPC;
using Content.Server.NPC.HTN;
using Content.Server.NPC.HTN.PrimitiveTasks;
using Content.Shared.Examine;
using Content.Shared.Mind;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Components;
using Content.Shared.NPC.Systems;
using Content.Site14.Shared.Blinking;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;

namespace Content.Site14.Server.NPC.HTN;

public sealed partial class ShyGuyTargetOperator : HTNOperator
{
    [Dependency] private readonly IEntityManager _entManager = default!;
    private NpcFactionSystem _faction = default!;
    private ExamineSystemShared _examine = default!;
    private SharedMindSystem _mind = default!;
    private SharedTransformSystem _transform = default!;
    private SharedAudioSystem _audio = default!;

    [DataField]
    public float SightRange = 50f;

    [DataField]
    public string TargetKey = "Target";

    [DataField]
    public string EnrageTimeKey = "EnrageTime";

    [DataField]
    public string CoordinatesKey = "TargetCoordinates";

    [DataField]
    public bool RequireMind = true;

    [DataField]
    public bool CheckBlinking = true;

    [DataField]
    public SoundSpecifier? EnrageSound;

    public override void Initialize(IEntitySystemManager sysManager)
    {
        base.Initialize(sysManager);
        _faction = sysManager.GetEntitySystem<NpcFactionSystem>();
        _examine = sysManager.GetEntitySystem<ExamineSystemShared>();
        _mind = sysManager.GetEntitySystem<SharedMindSystem>();
        _transform = sysManager.GetEntitySystem<SharedTransformSystem>();
        _audio = sysManager.GetEntitySystem<SharedAudioSystem>();
    }
    public override void Startup(NPCBlackboard blackboard)
    {
        base.Startup(blackboard);

        var owner = blackboard.GetValue<EntityUid>(NPCBlackboard.Owner);
        if (EnrageSound != null)
            _audio.PlayPvs(EnrageSound, owner);
    }

    public override async Task<(bool Valid, Dictionary<string, object>? Effects)> Plan(
        NPCBlackboard blackboard,
        CancellationToken cancelToken)
    {
        var owner = blackboard.GetValue<EntityUid>(NPCBlackboard.Owner);
        var target = FindLOSTarget(owner);

        if (target == null || EnrageSound == null)
            return (false, null);

        var resolvedSound = _audio.ResolveSound(EnrageSound);

        var effects = new Dictionary<string, object>
        {
            { TargetKey, target.Value },
            { EnrageTimeKey, (float) _audio.GetAudioLength(resolvedSound).TotalSeconds},
        };

        if (_entManager.TryGetComponent<TransformComponent>(target.Value, out var xform))
            effects[CoordinatesKey] = xform.Coordinates;
        return (true, effects);
    }

    public override HTNOperatorStatus Update(NPCBlackboard blackboard, float frameTime)
    {
        return HTNOperatorStatus.Finished;
    }

    private EntityUid? FindLOSTarget(EntityUid owner)
    {
        var hostiles = _faction.GetNearbyHostiles(owner, SightRange).ToList();
        var selfPos = _transform.GetMapCoordinates(owner);

        EntityUid? nearestTarget = null;
        var smallestDist = float.MaxValue;

        foreach (var hostile in hostiles)
        {
            if (!_entManager.TryGetComponent<MobStateComponent>(hostile, out var state)
                || state.CurrentState != MobState.Alive
                || RequireMind && !_mind.TryGetMind(hostile, out _, out _)
                || CheckBlinking && _entManager.TryGetComponent<BlinkingComponent>(hostile, out var blinking) && blinking.IsBlinking
                || !_examine.InRangeUnOccluded(hostile, owner, SightRange))
                continue;

            var targetPos = _transform.GetMapCoordinates(hostile);
            if (targetPos.MapId != selfPos.MapId)
                continue;

            var dist = (targetPos.Position - selfPos.Position).LengthSquared();
            if (!(dist < smallestDist))
                continue;
            smallestDist = dist;
            nearestTarget = hostile;
        }

        return nearestTarget;
    }
}
