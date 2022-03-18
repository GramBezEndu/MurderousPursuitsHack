namespace MurderousPursuitHack
{
    using MurderousPursuitHack.Input;
    using MurderousPursuitHack.Managers;
    using MurderousPursuitHack.Misc;
    using MurderousPursuitHack.Movement;
    using MurderousPursuitHack.WH;
    using MurderousPursuitHack.Windows;
    using UnityEngine;

    public class Loader
    {
        public static void Init()
        {
            Loader.Load = new GameObject();
            Loader.Load.AddComponent<InputManager>();
            Loader.Load.AddComponent<HackManager>();
            Loader.Load.AddComponent<SpeedhackManager>();
            Loader.Load.AddComponent<ExposureManager>();
            Loader.Load.AddComponent<AbilityManager>();
            Loader.Load.AddComponent<EspManager>();
            Loader.Load.AddComponent<Chams>();
            Loader.Load.AddComponent<AutoKill>();
            AddWindows();
            UnityEngine.Object.DontDestroyOnLoad(Loader.Load);
        }

        private static void AddWindows()
        {
            Loader.Load.AddComponent<DebugWindow>();
            Loader.Load.AddComponent<VisualsWindow>();
            Loader.Load.AddComponent<ColorPickerWindow>();
            Loader.Load.AddComponent<MovementWindow>();
            Loader.Load.AddComponent<MiscWindow>();
            Loader.Load.AddComponent<SettingsWindow>();
        }

        public static void Unload()
        {
            GameObject.Destroy(Load);
        }

        private static GameObject Load;
    }
}
