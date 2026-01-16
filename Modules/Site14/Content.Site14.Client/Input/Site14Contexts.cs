using Content.Site14.Common.Input;
using Robust.Shared.Input;

namespace Content.Site14.Client.Input;

public static class Site14Contexts
{
    public static void SetupContexts(IInputContextContainer contexts)
    {
        var common = contexts.GetContext("common");
        common.AddFunction(Site14KeyFunctions.HoldToFace);
    }
}
