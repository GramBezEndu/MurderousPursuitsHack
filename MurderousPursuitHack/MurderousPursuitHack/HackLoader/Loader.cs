namespace MurderousPursuitHack
{
    using MurderousPursuitHack.Input;
    using MurderousPursuitHack.Managers;
    using MurderousPursuitHack.WH;
    using UnityEngine;

    public class Loader
    {
        public static void Init()
        {
            Loader.Load = new GameObject();
            Loader.Load.AddComponent<InputManager>();
            Loader.Load.AddComponent<DebugManager>();
            Loader.Load.AddComponent<HackSettingsManager>();
            Loader.Load.AddComponent<GameInfoManager>();
            Loader.Load.AddComponent<SpeedhackManager>();
            Loader.Load.AddComponent<ExposureManager>();
            Loader.Load.AddComponent<WallhackManager>();
            Loader.Load.AddComponent<Glow>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.Load);
        }

        private static GameObject Load;
    }
}
