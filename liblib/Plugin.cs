using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using liblib.Core;
using liblib.Patches;
using liblib.Utilities;
using UnityEngine;

namespace liblib
{
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BasePlugin
    {
        public static bool HasLoaded;

        private static GameObject Root;
        public static Plugin Instance { get; private set; }
        public static ManualLogSource PluginLogger => Instance.Log;

        public override void Load()
        {
            Instance = this;

            Il2CppRegistrar.RegisterAllTypesInAssembly();
            PatchHandler.PatchAll();

            Libraries.RadiumWrapper.GameRoot.RootGameStarted += RootGameLoad;
        }

        private static void RootGameLoad()
        {
            if (HasLoaded) return;
            HasLoaded = true;

            Root = new GameObject(Constants.GUID);
            Root.AddComponent<Instance>();
            Root.RecRoomDontDestroyOnLoad();
        }
    }
}
