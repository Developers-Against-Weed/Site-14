// SPDX-FileCopyrightText: 2026 Site-14 Contributors
//
// SPDX-License-Identifier: MPL-2.0
//
// Additional Use Restrictions apply:
// See /LICENSES/SITE14-ADDENDUM.md

using Robust.Shared.GameStates;

namespace Content.Site14.Shared.Interaction.StopOnLOS;

[RegisterComponent, NetworkedComponent]
[AutoGenerateComponentState]
public sealed partial class StopOnLOSComponent : Component
{
    [AutoNetworkedField]
    public bool IsBeingObserved = false;

    [DataField]
    [AutoNetworkedField]
    public float SightRange = 12f;
}
