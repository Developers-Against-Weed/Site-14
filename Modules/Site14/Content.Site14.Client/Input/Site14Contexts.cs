// SPDX-FileCopyrightText: 2026 Site-14 Contributors
//
// SPDX-License-Identifier: MPL-2.0
//
// Additional Use Restrictions apply:
// See /LICENSES/SITE14-ADDENDUM.md

using Content.Site14.Common.Input;
using Robust.Shared.Input;

namespace Content.Site14.Client.Input;

public static class Site14Contexts
{
    public static void SetupContexts(IInputContextContainer contexts)
    {
        var common = contexts.GetContext("common");
        common.AddFunction(Site14KeyFunctions.HoldToFace);
        common.AddFunction(Site14KeyFunctions.Blink);
    }
}
