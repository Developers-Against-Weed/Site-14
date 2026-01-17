// Originally ported from Ephemeral Space: https://github.com/EphemeralSpace/ephemeral-space
//
// SPDX-License-Identifier: MIT-WIZARDS

using Content.Site14.Shared.Viewcone;

namespace Content.Site14.Client.Viewcone;

/// <summary>
///     Marks an entity which this client should always perceive, even if they have <see cref="ViewconeOccludableComponent"/>
/// </summary>ss
/// <remarks>
///     Used for dynamic situations where you should intuitively always show the occludable, like if you're pulling it.
/// </remarks>
[RegisterComponent]
public sealed partial class ViewconeClientNoOccludeComponent : Component;
