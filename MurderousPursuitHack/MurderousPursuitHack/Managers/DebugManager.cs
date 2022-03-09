namespace MurderousPursuitHack.Managers
{
    using BG.Utils;
    using MurderousPursuitHack.Drawing;
    using ProjectX.Levels;
    using ProjectX.Networking;
    using System;
    using System.Text;
    using UnityEngine;

    public class DebugManager : MonoBehaviour
    {
        private Vector2 windowPosition = new Vector2(10f, 5f);

        private Vector2 windowSize = new Vector2(300f, 240f);

        public void OnGUI()
        {
            if (HackSettingsManager.DebugInfo)
            {
                GUI.Window(1, new Rect(windowPosition, windowSize), CreateDebugWindow, "DEBUG");
            }
        }

        private void CreateDebugWindow(int windowID)
        {
            WindowBuilder builder = new WindowBuilder(windowPosition, windowSize, 30f);
            builder.StartElements();
            builder.Label("Mono memory: " + Math.Round(System.GC.GetTotalMemory(false) / Math.Pow(10, 6), 1) + " MB");
            builder.Label("Is Host: " + Singleton<LevelManager>.Instance.IsHost);
            builder.Label("Game Type: " + UNetManager.Instance.GameType);
            builder.Label("Quarry Bar found: " + ((GameInfoManager.Instance.QuarryBar != null) ? "True" : "False"));
            builder.Label("Hunter HUD found: " + ((GameInfoManager.Instance.HunterHUD != null) ? "True" : "False"));
            builder.Label("Hunters count: " + GameInfoManager.Instance.HunterIDs.Count.ToString());
        }
    }
}
