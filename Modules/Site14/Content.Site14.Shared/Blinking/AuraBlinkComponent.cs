using Robust.Shared.GameStates;

namespace Content.Site14.Shared.Blinking;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class AuraBlinkComponent : Component
{
    [DataField, AutoNetworkedField]
    public float Range = 12f;
}
