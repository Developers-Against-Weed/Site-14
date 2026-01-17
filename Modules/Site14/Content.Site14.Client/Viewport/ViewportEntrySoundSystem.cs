using System.Collections.Generic;
using Content.Shared.Examine;
using Content.Site14.Shared.Blinking;
using Content.Site14.Shared.Viewcone;
using Content.Site14.Shared.Viewport;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Player;
using Robust.Shared.Timing;

namespace Content.Site14.Client.Viewport;

public sealed class ViewportEntrySoundSystem : EntitySystem
{
    [Dependency] private readonly IEyeManager _eye = default!;
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly IPlayerManager _player = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly SharedTransformSystem _xform = default!;
    [Dependency] private readonly ViewconeEffectSystem _viewcone = default!;
    [Dependency] private readonly ExamineSystemShared _examine = default!;

    private readonly HashSet<EntityUid> _entitiesInViewport = new();

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ViewportEntrySoundComponent, ComponentShutdown>(OnShutdown);
        SubscribeLocalEvent<LocalPlayerDetachedEvent>(OnPlayerDetached);
    }

    private void OnShutdown(EntityUid uid, ViewportEntrySoundComponent comp, ComponentShutdown args)
    {
        _entitiesInViewport.Remove(uid);
    }

    private void OnPlayerDetached(LocalPlayerDetachedEvent args)
    {
        _entitiesInViewport.Clear();
    }

    public override void FrameUpdate(float frameTime)
    {
        base.FrameUpdate(frameTime);

        var localPlayer = _player.LocalEntity;
        if (localPlayer == null)
            return;

        if (TryComp<BlinkingComponent>(localPlayer, out var blinking) && blinking.IsBlinking)
            return;

        var viewport = _eye.GetWorldViewport();
        var currentTime = _timing.CurTime;
        var currentlyVisible = new HashSet<EntityUid>();

        var query = EntityQueryEnumerator<ViewportEntrySoundComponent, TransformComponent>();
        while (query.MoveNext(out var uid, out var comp, out var xform))
        {
            if (uid == localPlayer)
                continue;

            var worldPos = _xform.GetWorldPosition(xform);

            var shrunkViewport = viewport.Enlarged(-comp.ViewportOffset);
            if (!shrunkViewport.Contains(worldPos))
                continue;

            if (!_examine.InRangeUnOccluded(localPlayer.Value, uid))
                continue;

            if (!_viewcone.IsInViewcone(localPlayer.Value, uid))
                continue;

            currentlyVisible.Add(uid);

            if (_entitiesInViewport.Contains(uid))
                continue;

            if (comp.LastPlayTime != null && currentTime - comp.LastPlayTime < comp.Cooldown)
                continue;

            _audio.PlayGlobal(comp.Sound, Filter.Local(), false);
            comp.LastPlayTime = currentTime;
        }

        _entitiesInViewport.Clear();
        foreach (var uid in currentlyVisible)
            _entitiesInViewport.Add(uid);
    }
}
