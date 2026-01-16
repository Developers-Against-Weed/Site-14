// SPDX-FileCopyrightText: 2026 Space Station 14 Contributors
//
// SPDX-License-Identifier: MPL-2.0

namespace Content.Shared.Movement.Components
{
    public sealed partial class MovementSpeedModifierComponent : Component
    {
        public static Angle DefaultBackwardsAngle = Angle.FromDegrees(140);

        [DataField]
        public Angle BackwardsAngle = DefaultBackwardsAngle;
    }
}
