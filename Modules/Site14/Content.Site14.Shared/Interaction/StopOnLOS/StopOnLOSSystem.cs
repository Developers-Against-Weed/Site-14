using Content.Shared.ActionBlocker;
using Content.Shared.Examine;
using Content.Shared.Interaction.Events;
using Content.Shared.Mind;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Components;
using Content.Shared.Movement.Events;
using Content.Shared.NPC.Systems;
using Content.Site14.Shared.Blinking;
using Content.Site14.Shared.Viewcone;

namespace Content.Site14.Shared.Interaction.StopOnLOS;

public sealed class SharedStopOnLOSSystem : EntitySystem
{
    [Dependency] private readonly SharedMindSystem _mind = default!;
    [Dependency] private readonly ActionBlockerSystem _blocker = default!;
    [Dependency] private readonly NpcFactionSystem _faction = default!;
    [Dependency] private readonly ExamineSystemShared _examine = default!;
    [Dependency] private readonly ViewconeEffectSystem _viewcone = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<StopOnLOSComponent, UpdateCanMoveEvent>(OnAttempt);
        SubscribeLocalEvent<StopOnLOSComponent, AttackAttemptEvent>(OnAttempt);
    }

    public override void Update(float frameTime)
    {
        var query = EntityQueryEnumerator<StopOnLOSComponent>();
        while (query.MoveNext(out var entity, out var comp))
        {
            var isBeingObserved = false;

            foreach (var observer in _faction.GetNearbyHostiles(entity, comp.SightRange))
            {
                if (!TryComp<MobStateComponent>(observer, out var state))
                    continue;

                if (HasComp<StopOnLOSComponent>(observer))
                    continue;

                if (!_mind.TryGetMind(observer, out _, out _))
                    continue;

                if (state.CurrentState != MobState.Alive)
                    continue;

                if (TryComp<BlinkingComponent>(observer, out var blinking) && blinking.IsBlinking)
                    continue;

                if (!_viewcone.IsInViewcone(observer, entity))
                    continue;

                if (!_examine.InRangeUnOccluded(observer, entity, comp.SightRange, null))
                    continue;

                isBeingObserved = true;
                break;
            }

            if (comp.IsBeingObserved == isBeingObserved)
            {
                comp.IsBeingObserved = !isBeingObserved;
                _blocker.UpdateCanMove(entity);
            }
        }
    }

    private void OnAttempt(EntityUid uid, StopOnLOSComponent comp, CancellableEntityEventArgs args)
    {
        if (!comp.IsBeingObserved)
            args.Cancel();
    }
}
