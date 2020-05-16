using ProjectX.Abilities;
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
        public static float defaultRunMoveSpeed = 0f;
        public static float defaultFastWalkMoveSpeed = 0f;
        public static float nimbleRunMoveSpeed = 0f;
        public static float nimbleFastWalkMoveSpeed = 0f;
        public static float runMoveSpeed = 0f;
        public static float fastWalkMoveSpeed = 0f;
        public static float walkMoveSpeed = 0f;

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
        //TODO: Add XPlayerPerk (perk in general, search for it)
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
            builder.Append("Active abilities: " + '\n');
            //protected - used reflection
            MethodInfo getLoadoutMethod = CharacterAbilities.GetType().GetMethod("GetCurrentLoadout",
                BindingFlags.NonPublic | BindingFlags.Instance);
            LoadoutAbilityType[] loadoutAbilities = (LoadoutAbilityType[])(getLoadoutMethod.Invoke(CharacterAbilities, null));
            for (int i = 0; i < loadoutAbilities.Length; i++)
            {
                builder.Append(String.Format("Loadout ability {0}: {1}\n", i, loadoutAbilities[i]));
            }

            return builder.ToString();
        }
    }
}
