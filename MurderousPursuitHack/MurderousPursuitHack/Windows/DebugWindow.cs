namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Drawing;
    using MurderousPursuitHack.Managers;
    using ProjectX.Networking;
    using System;
    using UnityEngine;

    public class DebugWindow : MonoBehaviour
    {
        private Vector2 windowPosition = new Vector2(10f, 5f);

        private Vector2 windowSize = new Vector2(300f, 220f);

        private WindowBuilder builder;

        public void Start()
        {
            builder = new WindowBuilder(windowPosition, windowSize, 30f);
        }

        public void OnGUI()
        {
            void CreateDebugWindow(int windowID)
            {
                builder.Start();
                builder.Label("Mono memory: " + Math.Round(System.GC.GetTotalMemory(false) / Math.Pow(10, 6), 1) + " MB");
                builder.Label("Is Host: " + HackManager.Instance.IsHost.ToString());
                builder.Label("Game Type: " + UNetManager.Instance.GameType);
                builder.Label("Quarry Bar found: " + ((HackManager.Instance.QuarryBar != null) ? "True" : "False"));
                builder.Label("Hunter HUD found: " + ((HackManager.Instance.HunterHUD != null) ? "True" : "False"));
            }

            if (Settings.DebugWindow)
            {
                builder.CreateWindow(CreateDebugWindow, 0, "DEBUG");
            }
        }

        public void OnDestroy()
        {
            builder.OnDestroy();
        }
    }
}
