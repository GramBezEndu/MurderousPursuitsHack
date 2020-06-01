using ProjectX.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MurderousPursuitHack
{
    public static class TeleportManager
    {
        private static void TeleportLocalPlayer(Vector3 pos)
        {
            if (GameInfoManager.Instance != null)
            {
                var local = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer);
                if (local != null)
                {
                    Transform characterTransform = (Transform)(typeof(XCharacterMovement).GetField("characterTransform", GameInfoManager.FieldGetFlags).GetValue(local.CharacterMovement));
                    characterTransform.position = pos;
                }
            }
        }

        public static void TeleportLocalPlayerToQuarry()
        {
            if (GameInfoManager.Instance != null)
            {
                var target = GameInfoManager.Instance.Players.Find(x => x.IsQuarryForLocal);
                if (target != null)
                    TeleportLocalPlayer(target.Position);
            }
        }

        public static void TeleportLocalPlayerToHunter()
        {
            if (GameInfoManager.Instance != null)
            {
                var target = GameInfoManager.Instance.Players.Find(x => x.IsHunterForLocal);
                if (target != null)
                    TeleportLocalPlayer(target.Position);
            }
        }
    }
}
