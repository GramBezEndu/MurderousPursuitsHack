namespace MurderousPursuitHack.Managers
{
    using BG.Utils;
    using ProjectX.Levels;
    using ProjectX.Networking;
    using System;
    using System.Text;
    using UnityEngine;

    public class DebugManager : MonoBehaviour
    {
        private Vector2 windowPosition = new Vector2(10f, 5f);

        private Vector2 windowSize = new Vector2(300f, 220f);

        public void OnGUI()
        {
            if (HackSettingsManager.DebugInfo)
            {
                GUI.Window(1, new Rect(windowPosition, windowSize), CreateDebugWindow, "DEBUG");
            }
        }

        private void CreateDebugWindow(int windowID)
        {
            GUI.color = Color.white;
            GUI.Label(new Rect(windowPosition.x, 30, 800, 30), "Mono memory: " + Math.Round(System.GC.GetTotalMemory(false) / Math.Pow(10, 6), 1) + " MB");
            GUI.Label(new Rect(windowPosition.x, 60, 800, 30), "Is Host: " + Singleton<LevelManager>.Instance.IsHost);
            GUI.Label(new Rect(windowPosition.x, 90, 800, 30), "Game Type: " + UNetManager.Instance.GameType);
            GUI.Label(new Rect(windowPosition.x, 120, 800, 30), "Quarry Bar found: " + ((GameInfoManager.Instance.QuarryBar != null) ? "True" : "False"));
            GUI.Label(new Rect(windowPosition.x, 150, 800, 30), "Hunter HUD found: " + ((GameInfoManager.Instance.HunterHUD != null) ? "True" : "False"));
            GUI.Label(new Rect(windowPosition.x, 180, 800, 30), "Hunters count: " + GameInfoManager.Instance.HunterIDs.Count.ToString());
            GUI.color = Color.yellow;
        }
    }
}
