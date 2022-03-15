namespace MurderousPursuitHack
{
    using System;
    using System.Collections.Generic;

    public static class Settings
    {
        public static float[] SpeedhackMultipliers = GeneratePossibleMultipliers();

        public static int CurrentSpeedMultiplierIndex = 3;

        public static float SpeedMultiplier { get => SpeedhackMultipliers[CurrentSpeedMultiplierIndex]; }

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

        public static bool Speedhack { get; set; }

        public static bool ZeroExposure { get; set; } = true;

        public static float[] GeneratePossibleMultipliers()
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
