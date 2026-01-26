using JetBrains.Annotations;
using Robust.Shared.Serialization;

namespace Content.Site14.Shared.ShyGuy;

[Serializable, NetSerializable]
public enum ShyGuyState : byte
{
    Rest,
    Enraged,
    Charging,
}

[Serializable, NetSerializable]
public enum ShyGuyVisuals : byte
{
    State,
}

[Serializable, NetSerializable]
[UsedImplicitly] // jetbrains w
public enum ShyGuyVisualLayers : byte
{
     Base,
}
