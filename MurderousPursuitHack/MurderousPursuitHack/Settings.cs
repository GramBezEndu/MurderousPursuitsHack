namespace MurderousPursuitHack
{
    using System;

    public static class Settings
    {
        private static bool chamsEnabled = true;

        private static bool drawLocalPlayerChams = true;

        public static event EventHandler OnChamsDisabled;

        public static event EventHandler OnLocalChamsDisabled;

        public static bool DrawLocalPlayerChams 
        { 
            get => drawLocalPlayerChams;
            set
            {
                if (drawLocalPlayerChams != value)
                {
                    drawLocalPlayerChams = value;
                    if (drawLocalPlayerChams == false)
                    {
                        OnLocalChamsDisabled.Invoke(null, new EventArgs());
                    }
                }
            }
        }

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
