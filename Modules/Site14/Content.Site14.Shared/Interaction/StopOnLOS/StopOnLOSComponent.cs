using Robust.Shared.GameStates;

namespace Content.Site14.Shared.Interaction.StopOnLOS;

[RegisterComponent, NetworkedComponent]
[AutoGenerateComponentState]
public sealed partial class StopOnLOSComponent : Component
{
    [AutoNetworkedField]
    public bool IsBeingObserved = false;

    [DataField]
    [AutoNetworkedField]
    public float SightRange = 12f;
}
