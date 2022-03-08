﻿using MurderousPursuitHack.Drawing;
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

        public static bool ChamsEnabled = true;

        public static bool EspEnabled = true;

        public static bool DebugInfo = false;

        public static bool Speedhack = false;

        public static bool ZeroExposure = false;

        public static float[] SpeedhackMultipliers;

        public static int CurrentSpeedMultiplierIndex = 3; // Equals to 1f speed

        private Vector2 windowPosition = new Vector2(10f, 260f);

        private Vector2 windowSize = new Vector2(300, 610);

        private readonly float elementHeight = 25f;

        public void OnGUI()
        {
            if (!WindowHidden)
            {
                GUI.Window(0, new Rect(windowPosition, windowSize), CreateElements, String.Format("[{0}] CHEATS", InputManager.Instance.Keybindings.CheatWindow));
            }
        }

        private void CreateElements(int windowID)
        {
            WindowBuilder builder = new WindowBuilder(windowPosition, windowSize, elementHeight);
            builder.StartElements();

            VisualsSection(builder);
            TeleportsSection(builder);
            SpeedhackSection(builder);
            OthersSection(builder);
            AbilitiesSection(builder);
            DebugSection(builder);

            builder.EndElements();
        }

        private static void VisualsSection(WindowBuilder builder)
        {
            builder.Expander("VISUALS");

            ChamsEnabled = builder.Toggle(ChamsEnabled, String.Format("[{0}] Chams", InputManager.Instance.Keybindings.Chams));
            EspEnabled = builder.Toggle(EspEnabled, String.Format("[{0}] ESP", InputManager.Instance.Keybindings.Esp));
            if (builder.Button(String.Format("[{0}] Change skin", InputManager.Instance.Keybindings.ChangeSkin)))
            {
                SkinsHelper.ChangeSkin();
            }
        }

        private static void DebugSection(WindowBuilder builder)
        {
            builder.Expander("DEBUG");
            DebugInfo = builder.Toggle(DebugInfo, String.Format("[{0}] Debug window", InputManager.Instance.Keybindings.DebugInfo));
        }

        private static void TeleportsSection(WindowBuilder builder)
        {
            builder.Expander("TELEPORTS");
            if (builder.Button(String.Format("[{0}] Teleport To Quarry", InputManager.Instance.Keybindings.TeleportToQuarry)))
            {
                TeleportManager.TeleportToQuarry();
            }

            if (builder.Button(String.Format("[{0}] Teleport To Any Hunter", InputManager.Instance.Keybindings.TeleportToAnyHunter)))
            {
                TeleportManager.TeleportToAnyHunter();
            }
        }

        private static void SpeedhackSection(WindowBuilder builder)
        {
            builder.Expander("SPEED HACK");
            Speedhack = builder.Toggle(Speedhack, String.Format("Speedhack: {0}", Math.Round(SpeedhackMultipliers[CurrentSpeedMultiplierIndex], 3)));
            CurrentSpeedMultiplierIndex = (int)GUI.HorizontalSlider(builder.NextRect(),
                CurrentSpeedMultiplierIndex, 0, SpeedhackMultipliers.Length - 1);
        }

        private static void OthersSection(WindowBuilder builder)
        {
            builder.Expander("OTHERS");
            builder.StartDisabledSection();
            ZeroExposure = builder.Toggle(ZeroExposure, "[F5] Zero exposure");
            builder.EndDisabledSection();
        }

        private static void AbilitiesSection(WindowBuilder builder)
        {
            builder.Expander("ABILITIES");
            builder.StartDisabledSection();
            if (builder.Button("[F6] Place Pie Bomb"))
            {
                AbilityManager.StartAbility<XPlacePieBomb>();
            }

            if (builder.Button("[F7] Flash"))
            {
                AbilityManager.StartAbility<XFlash>();
            }

            if (builder.Button("[F8] Disrupt"))
            {
                AbilityManager.StartAbility<XDisrupt>();
            }

            builder.EndDisabledSection();
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
