// Originally ported from Ephemeral Space: https://github.com/EphemeralSpace/ephemeral-space
//
// SPDX-License-Identifier: MIT-WIZARDS

using System.Numerics;
using Robust.Shared.GameStates;

namespace Content.Site14.Shared.Viewcone;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class ViewconeComponent : Component
{
    [DataField, AutoNetworkedField]
    public float ConeAngle = 225f;

    [DataField, AutoNetworkedField]
    public float ConeFeather = 3f;

    [DataField, AutoNetworkedField]
    public float ConeIgnoreRadius = 0.65f;

    [DataField, AutoNetworkedField]
    public float ConeIgnoreFeather = 0.03f;

    // Clientside, used for lerping view angle
    // and keeping it consistent across all overlays
    public Angle ViewAngle;
    public Angle? DesiredViewAngle = null;
    public Angle LastMouseRotationAngle;
    public Vector2 LastWorldPos;
    public Angle LastWorldRotationAngle;
}
