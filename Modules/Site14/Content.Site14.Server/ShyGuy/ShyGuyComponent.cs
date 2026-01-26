// Content.Shared/ChargeOnLOS/ChargeOnLOSComponent.cs

using Robust.Shared.Audio;

namespace Content.Site14.Server.ShyGuy;

[RegisterComponent]
public sealed partial class ShyGuyComponent : Component
{
    [DataField]
    public SoundSpecifier? AttackSound;
}
