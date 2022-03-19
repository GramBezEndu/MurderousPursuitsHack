namespace MurderousPursuitHack
{
    using MurderousPursuitHack.Windows;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Settings
    {
        public static Settings Current { get => Default; }

        private static Settings Default { get; set; } = new Settings()
        {
            QuarryChams = true,
            HunterChams = true,
            NeutralChams = true,
            QuarryEsp = true,
            HunterEsp = true,
            NeutralEsp = true,
            CheatsWindow = true,
            ZeroExposure = true,
        };

        public static float[] SpeedhackMultipliers = GeneratePossibleMultipliers();

        #region Chams
        public event EventHandler OnQuarryChamsDisabled;

        public event EventHandler OnLocalChamsDisabled;

        public event EventHandler OnHunterChamsDisabled;

        public event EventHandler OnNeutralChamsDisabled;

        public bool QuarryChams
        {
            get => quarryChams;
            set
            {
                if (quarryChams != value)
                {
                    quarryChams = value;
                    if (quarryChams == false)
                    {
                        OnQuarryChamsDisabled?.Invoke(null, new EventArgs());
                    }
                }
            }
        }

        public bool LocalChams
        {
            get => localChams;
            set
            {
                if (localChams != value)
                {
                    localChams = value;
                    if (localChams == false)
                    {
                        OnLocalChamsDisabled.Invoke(null, new EventArgs());
                    }
                }
            }
        }

        public bool HunterChams
        {
            get => hunterChams;
            set
            {
                if (hunterChams != value)
                {
                    hunterChams = value;
                    if (hunterChams == false)
                    {
                        OnHunterChamsDisabled.Invoke(null, new EventArgs());
                    }
                }
            }
        }

        public bool NeutralChams
        {
            get => neutralChams;
            set
            {
                if (neutralChams != value)
                {
                    neutralChams = value;
                    if (neutralChams == false)
                    {
                        OnNeutralChamsDisabled.Invoke(null, new EventArgs());
                    }
                }
            }
        }

        private bool quarryChams;

        private bool localChams;

        private bool hunterChams;

        private bool neutralChams;

        public ColorData LocalGlow { get; set; } = new ColorData()
        {
            Description = "LOCAL PLAYER GLOW",
            R = 0,
            G = 100,
            B = 0,
            A = 255,
        };

        public ColorData QuarryGlow { get; set; } = new ColorData()
        {
            Description = "QUARRY GLOW",
            R = 0,
            G = 127,
            B = 178,
            A = 255,
        };

        public ColorData HunterGlow { get; set; } = new ColorData()
        {
            Description = "HUNTER GLOW",
            R = 140,
            G = 0,
            B = 0,
            A = 255,
        };

        public ColorData NeutralGlow { get; set; } = new ColorData()
        {
            Description = "NEUTRAL PLAYER GLOW",
            R = 0,
            G = 140,
            B = 0,
            A = 255,
        };

        #endregion
        #region Esp
        public ColorData NeutralEspColor { get; set; } = new ColorData()
        {
            Description = "NEUTRAL PLAYER",
            R = 0,
            G = 140,
            B = 0,
            A = 255,
        };

        public ColorData QuarryEspColor { get; set; } = new ColorData()
        {
            Description = "QUARRY",
            Color = Color.blue,
        };

        public ColorData HunterEspColor { get; set; } = new ColorData()
        {
            Description = "HUNTER",
            Color = Color.red,
        };

        public bool QuarryEsp { get; set; }

        public bool HunterEsp { get; set; }

        public bool NeutralEsp { get; set; }
        #endregion
        public int CurrentSpeedMultiplierIndex { get; set; } = 3;

        public float SpeedMultiplier { get => SpeedhackMultipliers[CurrentSpeedMultiplierIndex]; }

        public bool AutoAttackAfterTeleport { get; set; }

        public bool CheatsWindow { get; set; }

        public bool DebugWindow { get; set; }

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
