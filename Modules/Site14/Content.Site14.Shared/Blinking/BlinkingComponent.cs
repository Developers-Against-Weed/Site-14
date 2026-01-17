using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Site14.Shared.Blinking;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(true)]
public sealed partial class BlinkingComponent : Component
{
    [DataField, AutoNetworkedField]
    public TimeSpan LastBlinkTime;

    [DataField, AutoNetworkedField]
    public float MaxTimeWithoutBlink = 10f;

    [DataField, AutoNetworkedField]
    public float BlurStartTime = 3f;

    [DataField, AutoNetworkedField]
    public float MinClosedDuration = 0.1f;

    [DataField, AutoNetworkedField]
    public float MaxClosedDuration = 0.4f;

    [DataField, AutoNetworkedField]
    public float CloseAnimationTime = 0.08f;

    [DataField, AutoNetworkedField]
    public float OpenAnimationTime = 0.1f;

    [DataField, AutoNetworkedField]
    public bool IsBlinking;

    [DataField, AutoNetworkedField]
    public bool IsHoldingClosed;

    [DataField, AutoNetworkedField]
    public TimeSpan BlinkStartTime;

    [DataField, AutoNetworkedField]
    public float CurrentClosedDuration;

    [DataField, AutoNetworkedField]
    public bool AutoBlink = true;

    [DataField, AutoNetworkedField]
    public SoundSpecifier? BlinkSound;
}
