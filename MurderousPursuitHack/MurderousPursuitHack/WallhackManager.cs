using ProjectX.Camera;
using ProjectX.Characters;
using ProjectX.CoreGameLoop;
using ProjectX.Levels;
using ProjectX.Player;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MurderousPursuitHack
{
    class WallhackManager : MonoBehaviour
    {
        private static WallhackManager instance;
        public static WallhackManager Instance { get { return instance; } }

        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void Update()
        {
        }

        public void OnGUI()
        {
            GUI.color = Color.yellow;
            if (HackSettingsManager.DebugInfo)
                DrawDebugInfo();
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.IsGameRunning())
                {
                    if (HackSettingsManager.WallhackEnabled)
                        DrawWallhack();
                    if (HackSettingsManager.LocalPlayerInfo)
                        DisplayLocalPlayerInfo(GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer = true));
                }
            }
        }

        private void DrawWallhack()
        {
            foreach (var p in GameInfoManager.Instance.Players)
            {
                //Unity returns Vector3.zero when out off screen
                if (p.OnScreenPosition != Vector3.zero && p.OnScreenPosition.z >= 0)
                {
                    if (p.IsHunterForLocal)
                        GUI.color = PlayerInfo.Hunter;
                    else if (p.IsQuarryForLocal)
                        GUI.color = PlayerInfo.Quarry;
                    else
                        GUI.color = PlayerInfo.Neutral;
                    GUI.Label(new Rect(p.OnScreenPosition.x, Screen.height - p.OnScreenPosition.y, 200, 50), p.Name);
                }
            }
            //Bring back old color
            GUI.color = Color.yellow;
        }

        private static void DisplayLocalPlayerInfo(PlayerInfo p)
        {
            if (p.IsLocalPlayer)
            {
                GUI.Label(new Rect(Screen.width - 180, 0, 180, 800), p.ToString());
            }
        }

        private void DrawDebugInfo()
        {
            GUI.color = Color.red;
            GUI.Label(new Rect(0, 30, 400, 30), "DEBUG - Current scene: " + SceneManager.GetActiveScene().name);
            GUI.Label(new Rect(0, 60, 400, 30), "DEBUG - Player count: " + GameInfoManager.Instance.Players.Count);
            GUI.Label(new Rect(0, 90, 400, 30), "DEBUG - Mono memory: " + Math.Round(System.GC.GetTotalMemory(false) / Math.Pow(10, 6), 1) + " MB");
            GUI.Label(new Rect(0, 120, 400, 30), "DEBUG - Unity memory: " + Math.Round(Profiler.usedHeapSizeLong / Math.Pow(10, 6), 1) + " MB");
            GUI.color = Color.yellow;
        }
    }
}
