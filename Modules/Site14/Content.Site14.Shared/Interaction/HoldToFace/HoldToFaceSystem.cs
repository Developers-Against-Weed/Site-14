// Originally ported from Ephemeral Space: https://github.com/EphemeralSpace/ephemeral-space
//
// SPDX-License-Identifier: MIT-WIZARDS

using Content.Shared.CombatMode;
using Content.Site14.Common.Input;
using Robust.Shared.Input.Binding;
using Robust.Shared.Player;

namespace Content.Site14.Shared.Interaction.HoldToFace;

public sealed class HoldToFaceSystem : EntitySystem
{
    [Dependency] private readonly SharedCombatModeSystem _combat = default!;

    public override void Initialize()
    {
        base.Initialize();

        CommandBinds.Builder
            .Bind(Site14KeyFunctions.HoldToFace,
                InputCmdHandler.FromDelegate(args => ToggleRotator(args, true), args => ToggleRotator(args, false), false, false))
            .Register<HoldToFaceSystem>();
    }

    private void ToggleRotator(ICommonSession? session, bool value)
    {
        if (session?.AttachedEntity is not { } ent || !HasComp<HoldToFaceComponent>(ent))
            return;

        // Don't try and override combat mode doing the same thing
        if (TryComp<CombatModeComponent>(ent, out var combat) && combat is { ToggleMouseRotator: true, IsInCombatMode: true })
            return;

        _combat.SetMouseRotatorComponents(ent, value);
    }
}
