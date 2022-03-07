using MurderousPursuitHack.Drawing;
using MurderousPursuitHack.Input;
using MurderousPursuitHack.Skins;
using ProjectX.Abilities;
using System;
using System.Collections.Generic;
using UnityEngine;

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

        public static int CurrentSpeedMultiplierIndex = 3; // Equals to 1f speed

        private Vector2 windowPosition = new Vector2(10f, 230f);

        private Vector2 windowSize = new Vector2(300, 480);

        private readonly float elementHeight = 25f;

        public void OnGUI()
        {
            if (!WindowHidden)
            {
                GUI.Window(0, new Rect(windowPosition, windowSize), CreateCheatManagerWindow, "CHEAT MANAGER");
            }
        }

        private void CreateCheatManagerWindow(int windowID)
        {
            WindowBuilder builder = new WindowBuilder(windowPosition, windowSize, elementHeight);
            builder.StartWindow();

            builder.Label(String.Format(" [F1] Toggle window", InputManager.Instance.Keybindings.CheatWindow));

            builder.Label("VISUALS");

            WallhackEnabled = GUI.Toggle(
                builder.NextRect(),
                WallhackEnabled,
                String.Format(" [{0}] Wallhack", InputManager.Instance.Keybindings.Wallhack));

            builder.Label("DEBUG");
            DebugInfo = GUI.Toggle(
                builder.NextRect(),
                DebugInfo,
                String.Format(" {0} Debug window", InputManager.Instance.Keybindings.DebugInfo));

            builder.Label("TELEPORTS");
            if (GUI.Button(
                builder.NextRect(),
                String.Format(" [{0}] Teleport To Quarry", InputManager.Instance.Keybindings.TeleportToQuarry)))
            {
                TeleportManager.TeleportToQuarry();
            }

            if (GUI.Button(
                builder.NextRect(),
                String.Format(" [{0}] Teleport To Any Hunter", InputManager.Instance.Keybindings.TeleportToAnyHunter)))
            {
                TeleportManager.TeleportToAnyHunter();
            }

            builder.Label("SPEED HACK");
            Speedhack = GUI.Toggle(builder.NextRect(), Speedhack, String.Format("Speedhack: {0}", Math.Round(SpeedhackMultipliers[CurrentSpeedMultiplierIndex], 3)));
            CurrentSpeedMultiplierIndex = (int)GUI.HorizontalSlider(builder.NextRect(),
                CurrentSpeedMultiplierIndex, 0, SpeedhackMultipliers.Length - 1);

            builder.Label("OTHERS");
            GUI.enabled = false;
            ZeroExposure = GUI.Toggle(builder.NextRect(), ZeroExposure, " [F5] Zero exposure");

            GUI.enabled = true;
            builder.Label("ABILITIES");
            GUI.enabled = false;

            if (GUI.Button(builder.NextRect(), " [F6] Place Pie Bomb"))
            {
                AbilityManager.StartAbility<XPlacePieBomb>();
            }
            if (GUI.Button(builder.NextRect(), " [F7] Flash"))
            {
                AbilityManager.StartAbility<XFlash>();
            }

            if (GUI.Button(builder.NextRect(), " [F8] Disrupt"))
            {
                AbilityManager.StartAbility<XDisrupt>();
            }

            GUI.enabled = true;
            if (GUI.Button(builder.NextRect(), String.Format(" [{0}] Change skin", InputManager.Instance.Keybindings.ChangeSkin)))
                SkinsHelper.ChangeSkin();

            builder.EndWindow();
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
