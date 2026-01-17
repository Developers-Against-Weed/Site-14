// SPDX-FileCopyrightText: 2026 Site-14 Contributors
//
// SPDX-License-Identifier: MPL-2.0

using Content.Site14.Client.Input;
using Robust.Client.Input;
using Robust.Shared.ContentPack;
using Robust.Shared.Timing;

namespace Content.Site14.Client.Entry;

public sealed class EntryPoint : GameClient
{
    [Dependency] private readonly IInputManager _inputManager = default!;

    public override void Init()
    {
        IoCManager.BuildGraph();
        IoCManager.InjectDependencies(this);
    }

    public override void PostInit()
    {
        base.PostInit();

        Site14Contexts.SetupContexts(_inputManager.Contexts);
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
