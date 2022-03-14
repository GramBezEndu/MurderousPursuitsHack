namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Drawing;
    using MurderousPursuitHack.Input;
    using MurderousPursuitHack.Managers;
    using MurderousPursuitHack.Movement;
    using MurderousPursuitHack.Visuals;
    using ProjectX.Abilities;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class SettingsWindow : MonoBehaviour
    {
        public static float[] SpeedhackMultipliers;

        public static int CurrentSpeedMultiplierIndex = 3; // Equals to 1f speed

        private readonly float elementHeight = 25f;

        private WindowBuilder builder;

        private Vector2 windowPosition = new Vector2(10f, 260f);

        private Vector2 windowSize = new Vector2(300, 660);

        public void Start()
        {
            SpeedhackMultipliers = GeneratePossibleMultipliers();
            builder = new WindowBuilder(windowPosition, windowSize, elementHeight);
        }

        public void OnDestroy()
        {
            builder.OnDestroy();
        }

        public void OnGUI()
        {
            void CreateElements(int windowID)
            {
                builder.Start();
                VisualsSection(builder);
                TeleportsSection(builder);
                SpeedhackSection(builder);
                HostOnlySection(builder);
                DebugSection(builder);
            }

            if (Settings.CheatsWindow)
            {
                builder.CreateWindow(CreateElements, 1, "CHEATS");
            }
        }

        private void VisualsSection(WindowBuilder builder)
        {
            builder.StartSection("VISUALS");

            Settings.ChamsEnabled = builder.Toggle(Settings.ChamsEnabled, DrawingHelper.DisplayKeybind("Chams", InputManager.Instance.Keybindings.Chams));
            Settings.DrawLocalPlayerChams = 
                builder.Toggle(Settings.DrawLocalPlayerChams, DrawingHelper.DisplayKeybind("Local Player Chams", InputManager.Instance.Keybindings.LocalPlayerChams));
            Settings.EspEnabled = builder.Toggle(Settings.EspEnabled, DrawingHelper.DisplayKeybind("ESP", InputManager.Instance.Keybindings.Esp));
            if (builder.Button(DrawingHelper.DisplayKeybind("Change skin", InputManager.Instance.Keybindings.ChangeSkin)))
            {
                Skins.ChangeLocalPlayerSkin();
            }

            builder.EndSection();
        }

        private void DebugSection(WindowBuilder builder)
        {
            builder.StartSection("DEBUG");
            Settings.DebugWindow = builder.Toggle(Settings.DebugWindow, DrawingHelper.DisplayKeybind("Debug window", InputManager.Instance.Keybindings.DebugInfo));
            builder.EndSection();
        }

        private void TeleportsSection(WindowBuilder builder)
        {
            builder.StartSection("TELEPORTS");
            Settings.AutoAttackAfterTeleport = builder.Toggle(
                Settings.AutoAttackAfterTeleport,
                DrawingHelper.DisplayKeybind("Auto attack after teleport", InputManager.Instance.Keybindings.AutoAttackAfterTeleport));

            if (builder.Button(DrawingHelper.DisplayKeybind("Teleport to Quarry", InputManager.Instance.Keybindings.TeleportToQuarry)))
            {
                Teleports.TeleportToQuarry();
            }

            if (builder.Button(DrawingHelper.DisplayKeybind("Teleport to Closest Hunter", InputManager.Instance.Keybindings.TeleportToClosestHunter)))
            {
                Teleports.TeleportToClosestHunter();
            }

            if (builder.Button(DrawingHelper.DisplayKeybind("Teleport Quarry To Local", InputManager.Instance.Keybindings.TeleportQuarryToLocal)))
            {
                Teleports.TeleportQuarry();
            }

            if (builder.Button(DrawingHelper.DisplayKeybind("Teleport Hunter To Local", InputManager.Instance.Keybindings.TeleportHunter)))
            {
                Teleports.TeleportHunter();
            }

            builder.EndSection();
        }

        private void SpeedhackSection(WindowBuilder builder)
        {
            builder.StartSection("SPEED HACK");
            Settings.Speedhack = builder.Toggle(Settings.Speedhack, String.Format("Speedhack: {0}", Math.Round(SpeedhackMultipliers[CurrentSpeedMultiplierIndex], 3)));
            CurrentSpeedMultiplierIndex = builder.Slider(CurrentSpeedMultiplierIndex, 0, SpeedhackMultipliers.Length - 1);
            builder.EndSection();
        }

        private void HostOnlySection(WindowBuilder builder)
        {
            bool isHosting = HackManager.Instance.IsHost;
            builder.StartSection("HOST ONLY");
            Settings.ZeroExposure = builder.Toggle(Settings.ZeroExposure, DrawingHelper.DisplayKeybind("Zero Exposure", InputManager.Instance.Keybindings.ZeroExposure));
            if (!isHosting)
            {
                builder.StartDisabled();
            }

            if (builder.Button(DrawingHelper.DisplayKeybind("Pie Bomb", InputManager.Instance.Keybindings.PieBomb)))
            {
                Managers.AbilityManager.StartAbility<XPlacePieBomb>();
            }

            if (builder.Button(DrawingHelper.DisplayKeybind("Flash", InputManager.Instance.Keybindings.Flash)))
            {
                Managers.AbilityManager.StartAbility<XFlash>();
            }

            if (builder.Button(DrawingHelper.DisplayKeybind("Disrupt", InputManager.Instance.Keybindings.Disrupt)))
            {
                Managers.AbilityManager.StartAbility<XDisrupt>();
            }

            builder.EndSection();
            if (!isHosting)
            {
                builder.EndDisabled();
            }
        }

        private float[] GeneratePossibleMultipliers()
        {
            float min = 0.4f;
            float max = 5f;
            float step = 0.2f;
            List<float> results = new List<float>();
            while (min < max)
            {
                results.Add(min);
                min += step;
            }
            return results.ToArray();
        }
    }
}
