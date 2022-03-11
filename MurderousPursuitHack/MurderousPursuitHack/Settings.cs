using System;

namespace MurderousPursuitHack
{
    public static class Settings
    {
        private static bool chamsEnabled;

        public static event EventHandler OnChamsDisabled;

        public static bool CheatsWindow { get; set; }

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

        public static bool EspEnabled { get; set; }

        public static bool DebugWindow { get; set; }

        public static bool Speedhack { get; set; }

        public static bool ZeroExposure { get; set; }
    }
}
