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
            Builder.StartSection("TELEPORTS");
            Settings.AutoAttackAfterTeleport = Builder.Toggle(
                Settings.AutoAttackAfterTeleport,
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

            Builder.StartSection("SPEEDHACK");
            Settings.Speedhack = 
                Builder.Toggle(Settings.Speedhack, String.Format("Speedhack: {0}", Math.Round(Settings.SpeedMultiplier, 3)));
            Settings.CurrentSpeedMultiplierIndex = Builder.Slider(Settings.CurrentSpeedMultiplierIndex, 0, Settings.SpeedhackMultipliers.Length - 1);
            Builder.EndSection();
        }
    }
}
