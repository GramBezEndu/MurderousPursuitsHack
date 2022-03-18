namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Drawing;
    using MurderousPursuitHack.Input;
    using MurderousPursuitHack.Movement;
    using System;

    public class MovementWindow : Window
    {
        public static MovementWindow Instance { get; private set; }

        public override void Start()
        {
            base.Start();
            Name = "MOVEMENT";
            Instance = this;
        }

        protected override void CreateElements(int windowID)
        {
            Builder.Start();
            SpeedhackSection();
            TeleportsSection();
        }

        private void SpeedhackSection()
        {
            Builder.StartSection("SPEEDHACK", 100f);
            Settings.Current.Speedhack =
                Builder.Toggle(Settings.Current.Speedhack, String.Format("Speedhack: {0}", Math.Round(Settings.Current.SpeedMultiplier, 3)));
            Settings.Current.CurrentSpeedMultiplierIndex = Builder.Slider(Settings.Current.CurrentSpeedMultiplierIndex, 0, Settings.SpeedhackMultipliers.Length - 1);
            Builder.EndSection();

            Builder.StartSection("FLYHACK", 68f);
            if (Builder.Button(DrawingHelper.DisplayKeybind("Fly hack", InputManager.Instance.Keybindings.FlyHack)))
            {
                Managers.AbilityManager.Instance.ToggleFlyhack();
            }

            Builder.EndSection();
        }

        private void TeleportsSection()
        {
            Builder.StartSection("TELEPORTS", 210f);
            Settings.Current.AutoAttackAfterTeleport = Builder.Toggle(
                Settings.Current.AutoAttackAfterTeleport,
                DrawingHelper.DisplayKeybind("Auto attack after teleport", InputManager.Instance.Keybindings.AutoAttackAfterTeleport));

            if (Builder.Button(DrawingHelper.DisplayKeybind("Teleport to Quarry", InputManager.Instance.Keybindings.TeleportToQuarry)))
            {
                Teleports.TeleportToQuarry();
            }

            if (Builder.Button(DrawingHelper.DisplayKeybind("Teleport to Closest Hunter", InputManager.Instance.Keybindings.TeleportToClosestHunter)))
            {
                Teleports.TeleportToClosestHunter();
            }

            if (Builder.Button(DrawingHelper.DisplayKeybind("Teleport Quarry To Local", InputManager.Instance.Keybindings.TeleportQuarryToLocal)))
            {
                Teleports.TeleportQuarry();
            }

            if (Builder.Button(DrawingHelper.DisplayKeybind("Teleport Hunter To Local", InputManager.Instance.Keybindings.TeleportHunter)))
            {
                Teleports.TeleportHunter();
            }

            Builder.EndSection();
        }
    }
}
