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
            //Loader.Load.AddComponent<UnityMeshCreator>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.Load);
        }

        //public static void Unload()
        //{
        //    GameObject.Destroy(Load.GetComponent<HackSettingsManager>());
        //    GameObject.Destroy(Load.GetComponent<WallhackManager>());
        //}

        private static GameObject Load;
    }
}
