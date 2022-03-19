﻿namespace MurderousPursuitHack
{
    using ProjectX.Abilities;
    using ProjectX.Perks;
    using ProjectX.Player;
    using UnityEngine;

    public class PlayerData
    {
        public uint PlayerId { get; set; }

        public XPlayer Player { get; set; }

        public string DisplayName { get; set; }

        public bool IsAlive { get; set; }

        public bool IsBot { get; set; }

        public bool IsLocalPlayer { get; set; }

        public bool IsHunterForLocal { get; set; }

        public bool IsQuarryForLocal { get; set; }

        public Transform Transform { get; set; }

        // Vector3 is needed to check if player is behind camera
        public Vector3 OnScreenPosition { get; set; } = Vector3.zero;

        public CharacterAbilities CharacterAbilities { get; set; }

        public XPlayerPerk PlayerPerk { get; set; }

        public XCharacterMovement CharacterMovement { get; set; }
    }
}
