namespace MurderousPursuitHack
{
    using MurderousPursuitHack.Windows;
    using System;
    using System.Collections.Generic;

    public class Settings
    {
        public static Settings Current { get => Default; }

        private static Settings Default { get; set; } = new Settings()
        {
            ChamsEnabled = true,
            EspEnabled = true,
            CheatsWindow = true,
            ZeroExposure = true,
        };

        public static float[] SpeedhackMultipliers = GeneratePossibleMultipliers();

        private bool chamsEnabled;

        private bool drawLocalPlayerChams;

        public ColorData LocalChamsColor { get; set; } = new ColorData()
        {
            R = 0,
            G = 100,
            B = 0,
            A = 255,
        };

        public ColorData QuarryChams { get; set; } = new ColorData()
        {
            R = 0,
            G = 127,
            B = 178,
            A = 255,
        };

        public ColorData HunterChams { get; set; } = new ColorData()
        {
            R = 140,
            G = 0,
            B = 0,
            A = 255,
        };

        public ColorData NeutralChams { get; set; } = new ColorData()
        {
            R = 0,
            G = 140,
            B = 0,
            A = 255,
        };

        public event EventHandler OnChamsDisabled;

        public event EventHandler OnLocalChamsDisabled;

        public int CurrentSpeedMultiplierIndex { get; set; } = 3;

        public float SpeedMultiplier { get => SpeedhackMultipliers[CurrentSpeedMultiplierIndex]; }

        public bool DrawLocalPlayerChams
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

        public bool AutoAttackAfterTeleport { get; set; }

        public bool CheatsWindow { get; set; }

        public bool ChamsEnabled
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

        public bool EspEnabled { get; set; }

        public bool Speedhack { get; set; }

        public bool ZeroExposure { get; set; }

        private static float[] GeneratePossibleMultipliers()
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
