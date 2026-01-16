// Originally ported from Ephemeral Space: https://github.com/EphemeralSpace/ephemeral-space
//
// SPDX-License-Identifier: MIT-WIZARDS

using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Shared.Enums;

namespace Content.Site14.Client.Viewcone.Overlays;

/// <summary>
///     After <see cref="ViewconeSetAlphaOverlay"/> has run, resets the alpha of affected entities
///     back to normal.
/// </summary>
public sealed class ViewconeResetAlphaOverlay : Overlay
{
    [Dependency] private readonly IEntityManager _ent = default!;
    private readonly ViewconeOverlayManagementSystem _cone;
    private readonly SpriteSystem _sprite;

    public override OverlaySpace Space => OverlaySpace.WorldSpace;

    public ViewconeResetAlphaOverlay()
    {
        IoCManager.InjectDependencies(this);

        _cone = _ent.EntitySysManager.GetEntitySystem<ViewconeOverlayManagementSystem>();
        _sprite = _ent.EntitySysManager.GetEntitySystem<SpriteSystem>();
    }

    protected override void Draw(in OverlayDrawArgs args)
    {
        foreach (var (ent, baseAlpha) in _cone.CachedBaseAlphas)
        {
            _sprite.SetColor(ent!, ent.Comp.Color.WithAlpha(baseAlpha));
            _sprite.SetVisible(ent!, baseAlpha > 0f);
        }

        _cone.CachedBaseAlphas.Clear();
    }
}
