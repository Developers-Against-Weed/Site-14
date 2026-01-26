// SPDX-FileCopyrightText: 2026 Site-14 Contributors
//
// SPDX-License-Identifier: MPL-2.0
//
// Additional Use Restrictions apply:
// See /LICENSES/SITE14-ADDENDUM.md

// Content.Shared/ChargeOnLOS/ChargeOnLOSComponent.cs

using Robust.Shared.Audio;

namespace Content.Site14.Server.ShyGuy;

[RegisterComponent]
public sealed partial class ShyGuyComponent : Component
{
    [DataField]
    public SoundSpecifier? AttackSound;
}
