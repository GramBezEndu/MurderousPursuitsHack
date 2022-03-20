namespace MurderousPursuitHack
{
    using ProjectX.Abilities;
    using ProjectX.Perks;
    using ProjectX.Player;
    using System;
    using UnityEngine;

    public class PlayerData
    {
        public event EventHandler OnPlayerTypeChanged;

        private bool isHunterForLocal;

        private bool isQuarryForLocal;

        public uint PlayerId { get; set; }

        public XPlayer Player { get; set; }

        public string DisplayName { get; set; }

        public bool IsAlive { get; set; }

        public bool IsBot { get; set; }

        public bool IsLocalPlayer { get; set; }

        public bool IsHunterForLocal 
        { 
            get => isHunterForLocal;
            set
            {
                if (isHunterForLocal != value)
                {
                    isHunterForLocal = value;
                    OnPlayerTypeChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public bool IsQuarryForLocal 
        { 
            get => isQuarryForLocal; 
            set
            {
                if (isQuarryForLocal != value)
                {
                    isQuarryForLocal = value;
                    OnPlayerTypeChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public Transform Transform { get; set; }

        // Vector3 is needed to check if player is behind camera
        public Vector3 OnScreenPosition { get; set; } = Vector3.zero;

        public CharacterAbilities CharacterAbilities { get; set; }

        public XPlayerPerk PlayerPerk { get; set; }

        public XCharacterMovement CharacterMovement { get; set; }
    }
}
