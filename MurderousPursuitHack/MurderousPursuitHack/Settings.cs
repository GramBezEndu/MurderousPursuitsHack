﻿namespace MurderousPursuitHack
{
    using System;

    public static class Settings
    {
        private static bool chamsEnabled = true;

        public static event EventHandler OnChamsDisabled;

        public static bool AutoAttackAfterTeleport { get; set; } = true;

        public static bool CheatsWindow { get; set; } = true;

        public static bool ChamsEnabled
        {
            get => chamsEnabled;
            set
            {
                if (chamsEnabled != value)
                {
                    chamsEnabled = value;
                    if (chamsEnabled == false)
                    {
                        OnChamsDisabled?.Invoke(null, new EventArgs());
                    }
                }
            }
        }

        public static bool EspEnabled { get; set; } = true;

        public static bool DebugWindow { get; set; } = true;

        public static bool Speedhack { get; set; }

        public static bool ZeroExposure { get; set; } = true;
    }
}
