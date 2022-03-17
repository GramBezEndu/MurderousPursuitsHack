namespace MurderousPursuitHack.Input
{
    using UnityEngine;

    public class Keybindings
    {
        public KeyCode CheatWindow { get; set; } = KeyCode.F1;

        public KeyCode Chams { get; set; } = KeyCode.F2;

        public KeyCode FlyHack { get; set; } = KeyCode.F3;

        public KeyCode FreezeAnimation { get; set; } = KeyCode.F4;

        public KeyCode AutoKill { get; set; } = KeyCode.F6;

        public KeyCode LocalPlayerChams { get; set; } = KeyCode.None;

        public KeyCode PlayerESP { get; set; } = KeyCode.None;

        public KeyCode ChangeSkin { get; set; } = KeyCode.None;

        public KeyCode ZeroExposure { get; set; } = KeyCode.None;

        #region Abilities
        public KeyCode PieBomb { get; set; } = KeyCode.Z;

        public KeyCode Flash { get; set; } = KeyCode.X;

        public KeyCode Disrupt { get; set; } = KeyCode.C;
        #endregion

        #region Teleports
        public KeyCode AutoAttackAfterTeleport { get; set; } = KeyCode.None;

        public KeyCode TeleportToQuarry { get; set; } = KeyCode.Alpha1;

        public KeyCode TeleportToClosestHunter { get; set; } = KeyCode.Alpha2;

        public KeyCode TeleportQuarryToLocal { get; set; } = KeyCode.Alpha3;

        public KeyCode TeleportHunter { get; set; } = KeyCode.Alpha4;
        #endregion
    }
}
