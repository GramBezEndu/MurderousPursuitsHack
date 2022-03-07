using BG.Utils;
using MurderousPursuitHack.Input;
using MurderousPursuitHack.Skins;
using ProjectX.Abilities;
using ProjectX.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MurderousPursuitHack
{
    class HackSettingsManager : MonoBehaviour
    {
        public static bool WindowHidden = false;

        public static bool WallhackEnabled = true;

        public static bool DebugInfo = false;

        public static bool Speedhack = false;

        public static bool ZeroExposure = false;

        public static float[] SpeedhackMultipliers;

        public static int CurrentMultiplier = 3; //1f

        private Vector2 windowPosition = new Vector2(10f, 230f);

        private Vector2 windowSize = new Vector2(300, 480);

        private readonly float yMargin = 25f;

        public void OnGUI()
        {
            if (!WindowHidden)
            {
                GUI.Window(0, new Rect(windowPosition, windowSize), CreateCheatManagerWindow, "CHEAT MANAGER");
            }
        }

        private void CreateCheatManagerWindow(int windowID)
        {
            GUI.Label(
                new Rect(windowPosition.x, yMargin, windowSize.x - 2 * windowPosition.x, yMargin),
                String.Format(" [F1] Toggle window", InputManager.Instance.Keybindings.CheatWindow));

            GUI.Label(new Rect(windowPosition.x, 2 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin), "VISUALS");
            WallhackEnabled = GUI.Toggle(
                new Rect(windowPosition.x, 3 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin),
                WallhackEnabled,
                String.Format(" [{0}] Wallhack", InputManager.Instance.Keybindings.Wallhack));

            GUI.Label(new Rect(windowPosition.x, 4 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin), "DEBUG");
            DebugInfo = GUI.Toggle(
                new Rect(windowPosition.x, 6 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin),
                DebugInfo,
                String.Format(" {0} Debug window", InputManager.Instance.Keybindings.DebugInfo));

            GUI.Label(new Rect(windowPosition.x, 7 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin), "TELEPORTS");
            GUI.enabled = false;
            if (GUI.Button(new Rect(windowPosition.x, 8 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin), " [Num1] Teleport to quarry"))
                TeleportManager.TeleportLocalPlayerToQuarry();
            if (GUI.Button(new Rect(windowPosition.x, 9 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin), " [Num2] Teleport to any hunter"))
                TeleportManager.TeleportLocalPlayerToHunter();
            GUI.enabled = true;

            GUI.Label(new Rect(windowPosition.x, 10 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin), "SPEED HACK");
            Speedhack = GUI.Toggle(new Rect(windowPosition.x, 11 * yMargin, windowSize.x * 0.4f, yMargin), Speedhack, String.Format("Speedhack: {0}", Math.Round(SpeedhackMultipliers[CurrentMultiplier], 3)));
            CurrentMultiplier = (int)GUI.HorizontalSlider(new Rect(windowPosition.x + windowSize.x * 0.45f, 11 * yMargin + 5, windowSize.x * 0.4f, yMargin),
                CurrentMultiplier, 0, SpeedhackMultipliers.Length - 1);

            GUI.Label(new Rect(windowPosition.x, 12 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin), "OTHERS");
            GUI.enabled = false;
            ZeroExposure = GUI.Toggle(new Rect(windowPosition.x, 13 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin), ZeroExposure, " [F5] Zero exposure");
            GUI.enabled = true;

            GUI.Label(new Rect(windowPosition.x, 14 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin), "ABILITIES");
            GUI.enabled = false;
            if (GUI.Button(new Rect(windowPosition.x, 15 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin), " [F6] Place Pie Bomb"))
                AbilityManager.StartAbility<XPlacePieBomb>();
            if (GUI.Button(new Rect(windowPosition.x, 16 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin), " [F7] Flash"))
                AbilityManager.StartAbility<XFlash>();
            if (GUI.Button(new Rect(windowPosition.x, 17 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin), " [F8] Disrupt"))
                AbilityManager.StartAbility<XDisrupt>();
            GUI.enabled = true;
            if (GUI.Button(new Rect(
                windowPosition.x, 17 * yMargin, windowSize.x - 2 * windowPosition.x, yMargin),
                String.Format(" [{0}] Change skin", InputManager.Instance.Keybindings.ChangeSkin)))
                SkinsHelper.ChangeSkin();
        }

        public void Start()
        {
            SpeedhackMultipliers = GeneratePossibleMultipliers();
        }

        private float[] GeneratePossibleMultipliers()
        {
            float min = 0.4f;
            float max = 5f;
            float step = 0.2f;
            List<float> results = new List<float>();
            while(min < max)
            {
                results.Add(min);
                min += step;
            }
            return results.ToArray();
        }
    }
}
