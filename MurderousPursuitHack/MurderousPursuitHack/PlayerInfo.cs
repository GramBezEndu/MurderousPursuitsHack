using ProjectX.Abilities;
using ProjectX.Perks;
using ProjectX.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MurderousPursuitHack
{
    public class PlayerInfo
    {
        public static Color Neutral = Color.green;
        public static Color Hunter = Color.red;
        public static Color Quarry = Color.blue;

        //Base values
        public static float defaultRunMoveSpeed;
        public static float defaultFastWalkMoveSpeed;
        public static float nimbleRunMoveSpeed;
        public static float nimbleFastWalkMoveSpeed;
        public static float runMoveSpeed;
        public static float fastWalkMoveSpeed;
        public static float walkMoveSpeed;

        public string Name;
        public bool IsLocalPlayer;
        public bool IsHunterForLocal;
        public bool IsQuarryForLocal;
        public Vector3 Position = Vector3.zero;
        //We need vector3 to check Z (behind camera)
        public Vector3 OnScreenPosition = Vector3.zero;
        public Vector3 Velocity = Vector3.zero;
        public Vector3 Size = Vector3.zero;
        public CharacterAbilities CharacterAbilities;
        public XPlayerPerk PlayerPerk;
        public Collider Collider;
        public XCharacterMovement CharacterMovement;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Name + '\n');
            builder.Append("Is local player: " + IsLocalPlayer + '\n');
            builder.Append("Is hunter for local: " + IsHunterForLocal + '\n');
            builder.Append("Is quarry for local: " + IsQuarryForLocal + '\n');
            builder.Append("Pos: " + Position + '\n');
            builder.Append("Velocity: " + Velocity + '\n');
            builder.Append("Size: " + Size + '\n');
            builder.Append(String.Format("Perk: {0}", PlayerPerk.CurrentPerk));

            return builder.ToString();
        }
    }
}
