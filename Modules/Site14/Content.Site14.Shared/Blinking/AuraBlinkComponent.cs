// SPDX-FileCopyrightText: 2026 Site-14 Contributors
//
// SPDX-License-Identifier: MPL-2.0
//
// Additional Use Restrictions apply:
// See /LICENSES/SITE14-ADDENDUM.md

using Robust.Shared.GameStates;

namespace Content.Site14.Shared.Blinking;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class AuraBlinkComponent : Component
{
    [DataField, AutoNetworkedField]
    public float Range = 12f;
}
