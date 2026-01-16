// Originally ported from Ephemeral Space: https://github.com/EphemeralSpace/ephemeral-space
//
// SPDX-License-Identifier: MIT-WIZARDS

using Content.Shared.MouseRotator;
using Robust.Shared.GameStates;

namespace Content.Site14.Shared.Interaction.HoldToFace;

/// <summary>
///     Marks an entity as being able to hold a key to face a certain direction while that key is held.
///     Just adds <see cref="MouseRotatorComponent"/> for the duration of the key being held.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class HoldToFaceComponent : Component
{
}
