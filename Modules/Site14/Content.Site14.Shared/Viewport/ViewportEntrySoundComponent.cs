using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Site14.Shared.Viewport;

/// <summary>
/// Plays a sound effect when this entity enters the local player's viewport,
/// provided the player is not blinking and the entity is within their viewcone.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class ViewportEntrySoundComponent : Component
{
    [DataField(required: true), AutoNetworkedField]
    public SoundSpecifier Sound = default!;

    [DataField, AutoNetworkedField]
    public TimeSpan Cooldown = TimeSpan.FromMinutes(10);

    [DataField, AutoNetworkedField]
    public float ViewportOffset = 1f;

    [ViewVariables]
    public TimeSpan? LastPlayTime;
}
