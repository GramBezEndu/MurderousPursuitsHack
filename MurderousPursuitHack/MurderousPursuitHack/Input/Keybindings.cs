﻿namespace MurderousPursuitHack.Input
{
    using UnityEngine;

    public class Keybindings
    {
        public KeyCode CheatWindow { get; set; } = KeyCode.F1;

        public KeyCode Chams { get; set; } = KeyCode.None;

        public KeyCode LocalPlayerChams { get; set; } = KeyCode.None;

        public KeyCode Esp { get; set; } = KeyCode.None;

        public KeyCode ChangeSkin { get; set; } = KeyCode.F2;

        public KeyCode DebugInfo { get; set; } = KeyCode.F5;

        public KeyCode ZeroExposure { get; set; } = KeyCode.None;

        public KeyCode PieBomb { get; set; } = KeyCode.Z;

        public KeyCode Flash { get; set; } = KeyCode.X;

        public KeyCode Disrupt { get; set; } = KeyCode.C;

        public KeyCode AutoAttackAfterTeleport { get; set; } = KeyCode.None;

        public KeyCode TeleportToQuarry { get; set; } = KeyCode.Alpha1;

        public KeyCode TeleportToClosestHunter { get; set; } = KeyCode.Alpha2;

        public KeyCode TeleportQuarryToLocal { get; set; } = KeyCode.Alpha3;

        public KeyCode TeleportHunter { get; set; } = KeyCode.Alpha4;
    }
}
