﻿namespace MurderousPursuitHack
{
    using ProjectX.Abilities;
    using ProjectX.Perks;
    using ProjectX.Player;
    using UnityEngine;

    public class PlayerInfo
    {
        //Base values
        public static float defaultRunMoveSpeed;
        public static float defaultFastWalkMoveSpeed;
        public static float nimbleRunMoveSpeed;
        public static float nimbleFastWalkMoveSpeed;
        public static float runMoveSpeed;
        public static float fastWalkMoveSpeed;
        public static float walkMoveSpeed;

        public uint PlayerId { get; set; }

        public XPlayer Player { get; set; }

        public string DisplayName { get; set; }

        public bool IsAlive { get; set; }

        public bool IsBot { get; set; }

        public bool IsLocalPlayer { get; set; }

        public bool IsHunterForLocal { get; set; }

        public bool IsQuarryForLocal { get; set; }

        public Vector3 Position { get; set; } = Vector3.zero;

        //We need vector3 to check Z (behind camera)
        public Vector3 OnScreenPosition { get; set; } = Vector3.zero;

        public Vector3 Velocity { get; set; } = Vector3.zero;

        public Vector3 Size { get; set; } = Vector3.zero;

        public CharacterAbilities CharacterAbilities { get; set; }

        public XPlayerPerk PlayerPerk { get; set; }

        public Collider Collider { get; set; }

        public XCharacterMovement CharacterMovement { get; set; }
    }
}