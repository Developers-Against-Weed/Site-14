// Originally ported from Ephemeral Space: https://github.com/EphemeralSpace/ephemeral-space
//
// SPDX-License-Identifier: MIT-WIZARDS

using Content.Site14.Shared.Viewcone;
using Robust.Shared.ComponentTrees;
using Robust.Shared.Physics;

namespace Content.Site14.Client.Viewcone.ComponentTree;

[RegisterComponent]
public sealed partial class ViewconeOccludableTreeComponent : Component, IComponentTreeComponent<ViewconeOccludableComponent>
{
    public DynamicTree<ComponentTreeEntry<ViewconeOccludableComponent>> Tree { get; set; } = null!;
}
