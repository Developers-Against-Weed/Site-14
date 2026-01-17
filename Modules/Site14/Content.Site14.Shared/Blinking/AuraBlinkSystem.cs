using Content.Shared.Examine;

namespace Content.Site14.Shared.Blinking;

public sealed class AuraBlinkSystem : EntitySystem
{
    [Dependency] private readonly EntityLookupSystem _lookup = default!;
    [Dependency] private readonly ExamineSystemShared _examine = default!;

    private readonly HashSet<Entity<BlinkingComponent>> _nearbyEntities = new();
    private readonly HashSet<EntityUid> _affectedEntities = new();

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var currentlyAffected = new HashSet<EntityUid>();

        var auraQuery = EntityQueryEnumerator<AuraBlinkComponent, TransformComponent>();
        while (auraQuery.MoveNext(out var auraUid, out var aura, out var auraXform))
        {
            _nearbyEntities.Clear();
            _lookup.GetEntitiesInRange(auraXform.Coordinates, aura.Range, _nearbyEntities);

            foreach (var (blinkUid, blinkComp) in _nearbyEntities)
            {
                if (blinkUid == auraUid)
                    continue;

                if (!_examine.InRangeUnOccluded(auraUid, blinkUid, aura.Range, null))
                    continue;

                currentlyAffected.Add(blinkUid);

                if (!blinkComp.AutoBlink)
                {
                    blinkComp.AutoBlink = true;
                    Dirty(blinkUid, blinkComp);
                }
            }
        }

        foreach (var uid in _affectedEntities)
        {
            if (currentlyAffected.Contains(uid))
                continue;

            if (!TryComp<BlinkingComponent>(uid, out var blinkComp))
                continue;

            if (blinkComp.AutoBlink)
            {
                blinkComp.AutoBlink = false;
                Dirty(uid, blinkComp);
            }
        }

        _affectedEntities.Clear();
        foreach (var uid in currentlyAffected)
            _affectedEntities.Add(uid);
    }
}
