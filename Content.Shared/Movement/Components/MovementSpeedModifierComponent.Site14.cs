namespace Content.Shared.Movement.Components
{
    public sealed partial class MovementSpeedModifierComponent : Component
    {
        public static Angle DefaultBackwardsAngle = Angle.FromDegrees(140);

        [DataField]
        public Angle BackwardsAngle = DefaultBackwardsAngle;
    }
}
