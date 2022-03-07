﻿namespace MurderousPursuitHack
{
    using ProjectX.Player;
    using System.Collections.Generic;
    using UnityEngine;

    public static class TeleportManager
    {
        public static void TeleportToQuarry()
        {
            if (GameInfoManager.Instance != null)
            {
                var target = GameInfoManager.Instance.Players.Find(x => x.IsQuarryForLocal);
                if (target != null)
                    TeleportLocalPlayer(target.Position);
            }
        }

        public static void TeleportToAnyHunter()
        {
            if (GameInfoManager.Instance != null)
            {
                List<PlayerInfo> hunters = GameInfoManager.Instance.Players.FindAll(x => x.IsHunterForLocal);
                if (hunters != null)
                {
                    foreach (var hunter in hunters)
                    {
                        if (hunter.IsAlive)
                        {
                            TeleportLocalPlayer(hunter.Position);
                            return;
                        }
                    }
                }
            }
        }

        private static void TeleportLocalPlayer(Vector3 pos)
        {
            if (GameInfoManager.Instance != null)
            {
                PlayerInfo local = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer);
                if (local != null)
                {
                    Transform characterTransform = (Transform)(typeof(XCharacterMovement).GetField("characterTransform", Utils.FieldGetFlags).GetValue(local.CharacterMovement));
                    characterTransform.position = pos;
                }
            }
        }
    }
}