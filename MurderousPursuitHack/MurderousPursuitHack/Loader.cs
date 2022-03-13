namespace MurderousPursuitHack
{
    using MurderousPursuitHack.Input;
    using MurderousPursuitHack.Managers;
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
            Loader.Load.AddComponent<DebugWindow>();
            Loader.Load.AddComponent<SettingsWindow>();
            Loader.Load.AddComponent<HackManager>();
            Loader.Load.AddComponent<SpeedhackManager>();
            Loader.Load.AddComponent<ExposureManager>();
            Loader.Load.AddComponent<EspManager>();
            Loader.Load.AddComponent<Chams>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.Load);
        }

        public static void Unload()
        {
            GameObject.Destroy(Load);
        }

        private static GameObject Load;
    }
}
