// SPDX-FileCopyrightText: 2026 Site-14 Contributors
//
// SPDX-License-Identifier: MPL-2.0
//
// Additional Use Restrictions apply:
// See /LICENSES/SITE14-ADDENDUM.md

using JetBrains.Annotations;
using Robust.Shared.Serialization;

namespace Content.Site14.Shared.ShyGuy;

[Serializable, NetSerializable]
public enum ShyGuyState : byte
{
    Rest,
    Enraged,
    Charging,
}

[Serializable, NetSerializable]
public enum ShyGuyVisuals : byte
{
    State,
}

[Serializable, NetSerializable]
[UsedImplicitly] // jetbrains w
public enum ShyGuyVisualLayers : byte
{
     Base,
}
