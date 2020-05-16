using System;
using UnityEngine;

namespace MurderousPursuitHack
{
    public class Loader
    {
        public static void Init()
        {
            Loader.Load = new GameObject();
            Loader.Load.AddComponent<HackSettingsManager>();
            Loader.Load.AddComponent<GameInfoManager>();
            Loader.Load.AddComponent<WallhackManager>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.Load);
        }

        private static GameObject Load;
    }
}
