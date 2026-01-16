// Originally ported from Ephemeral Space: https://github.com/EphemeralSpace/ephemeral-space
//
// SPDX-License-Identifier: MIT-WIZARDS

namespace Content.Site14.Common.Viewcone.Events;

/// <summary>
/// Raised to spawn a viewcone effect.
/// </summary>
[ByRefEvent]
public record struct SpawnViewconeEffectEvent(EntityUid Uid, Angle Direction)
{
    public readonly EntityUid Uid = Uid;

    public Angle Direction = Direction;
}
