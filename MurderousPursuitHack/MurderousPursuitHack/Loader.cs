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
            Loader.Load.AddComponent<SpeedhackManager>();
            Loader.Load.AddComponent<ZeroExposure>();
            //UnityEngine.Object.DontDestroyOnLoad(Loader.Load);

            ////disable logs (might improve performance)
            //Debug.logger.logEnabled = false;
        }

        private static GameObject Load;
    }
}
