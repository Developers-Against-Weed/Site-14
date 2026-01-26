using Content.Server.NPC.HTN;
using Content.Site14.Shared.ShyGuy;
using Robust.Shared.Map;

namespace Content.Site14.Server.ShyGuy;

/// <summary>
/// This handles...
/// </summary>
public sealed class ShyGuySystem : EntitySystem
{
    [Dependency] private readonly SharedAppearanceSystem _appearance = default!;

    private const float DelayBetweenEnrageAndChargeState = 1f;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<ShyGuyComponent, HTNComponent, AppearanceComponent>();
        while (query.MoveNext(out var uid, out _, out var htn, out var appearance))
        {
            var hasTarget = htn.Blackboard.TryGetValue<EntityUid>("Target", out _, EntityManager);

            if (!hasTarget)
                _appearance.SetData(uid, ShyGuyVisuals.State, ShyGuyState.Rest, appearance);
            else
            {
                var isWaiting = htn.Blackboard.TryGetValue<float>("EnrageTime", out var time, EntityManager)
                                && time > DelayBetweenEnrageAndChargeState;
                _appearance.SetData(uid,
                    ShyGuyVisuals.State,
                    isWaiting ? ShyGuyState.Enraged : ShyGuyState.Charging,
                    appearance);
            }
        }
    }
}
