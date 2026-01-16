// SPDX-FileCopyrightText: 2026 Site-14 Contributors
//
// SPDX-License-Identifier: MPL-2.0

using Content.Site14.Server.IoC;
using Robust.Shared.ContentPack;
using Robust.Shared.Timing;

namespace Content.Site14.Server.Entry;

public sealed class EntryPoint : GameServer
{

    public override void Init()
    {
        base.Init();
        ServerSite14ContentIoC.Register();
        IoCManager.BuildGraph();
    }

    public override void PostInit()
    {
        base.PostInit();
    }

    public override void Update(ModUpdateLevel level, FrameEventArgs frameEventArgs)
    {
        base.Update(level, frameEventArgs);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
}
