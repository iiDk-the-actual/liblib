using HarmonyLib;

namespace liblib.Libraries.RadiumWrapper.Patches;

[HarmonyPatch(typeof(RecRoomGameRoot), nameof(RecRoomGameRoot.Awake))]
internal static class RootLoad
{
    [HarmonyPostfix]
    private static void Postfix()
    {
        GameRoot.InternalInvokeRootGameStarted();
    }
}