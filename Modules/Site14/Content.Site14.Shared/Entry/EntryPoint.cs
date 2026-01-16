// SPDX-FileCopyrightText: 2026 Site-14 Contributors
//
// SPDX-License-Identifier: MPL-2.0

using Content.Site14.Shared.IoC;
using Robust.Shared.ContentPack;

namespace Content.Site14.Shared.Entry;

public sealed class EntryPoint : GameShared
{
    public override void PreInit()
    {
        IoCManager.InjectDependencies(this);
        SharedSite14ContentIoC.Register();
    }
}
