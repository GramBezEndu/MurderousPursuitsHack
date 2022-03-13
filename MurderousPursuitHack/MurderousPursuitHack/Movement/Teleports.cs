namespace MurderousPursuitHack.Movement
{
    using MurderousPursuitHack.Managers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class Teleports
    {
        public static bool TeleportToQuarry()
        {
            if (!HackManager.Instance.InGame)
            {
                return false;
            }

            PlayerData target = HackManager.Instance.Players.Find(x => x.IsQuarryForLocal);
            if (target == null)
            {
                return false;
            }

            return TeleportLocalToPlayer(target, Settings.AutoAttackAfterTeleport);
        }

        public static bool TeleportToClosestHunter()
        {
            if (!HackManager.Instance.InGame)
            {
                return false;
            }

            PlayerData closestHunter = GetClosestHunter();
            if (closestHunter == null)
            {
                return false;
            }

            return TeleportLocalToPlayer(closestHunter, Settings.AutoAttackAfterTeleport);
        }

        public static bool TeleportQuarry()
        {
            if (!HackManager.Instance.InGame)
            {
                return false;
            }

            PlayerData quarry = HackManager.Instance.Players.Find(x => x.IsQuarryForLocal);
            if (quarry == null)
            {
                return false;
            }

            return TeleportPlayerToLocal(quarry, Settings.AutoAttackAfterTeleport);
        }

        public static bool TeleportHunter()
        {
            if (!HackManager.Instance.InGame)
            {
                return false;
            }

            PlayerData closestHunter = GetClosestHunter();
            if (closestHunter == null)
            {
                return false;
            }

            return TeleportPlayerToLocal(closestHunter, Settings.AutoAttackAfterTeleport);
        }

        private static bool TeleportPlayerToLocal(PlayerData from, bool attackAutomatically)
        {
            if (TeleportPlayer(from, HackManager.Instance.Players.Find(x => x.IsLocalPlayer).Position))
            {
                if (attackAutomatically)
                {
                    return AbilityManager.StartAttack(from.Player);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private static bool TeleportLocalToPlayer(PlayerData to, bool attackAutomatically)
        {
            if (TeleportLocalPlayer(to.Position))
            {
                if (attackAutomatically)
                {
                    return AbilityManager.StartAttack(to.Player);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private static bool TeleportLocalPlayer(Vector3 position)
        {
            return TeleportPlayer(HackManager.Instance.Players.Find(x => x.IsLocalPlayer), position);
        }

        private static bool TeleportPlayer(PlayerData playerData, Vector3 position)
        {
            if (playerData == null)
            {
                return false;
            }

            Transform characterTransform = (Transform)(playerData.CharacterMovement.GetFieldValue("characterTransform"));
            if (characterTransform == null)
            {
                return false;
            }

            characterTransform.position = position;
            return true;
        }

        private static PlayerData GetClosestHunter()
        {
            PlayerData[] hunters = HackManager.Instance.Players.FindAll(x => x.IsHunterForLocal).ToArray();
            if (hunters == null || hunters.Length == 0)
            {
                return null;
            }

            if (HackManager.Instance.LocalPlayer == null)
            {
                return null;
            }

            Vector3 localPosition = HackManager.Instance.LocalPlayer.transform.position;
            float[] distanceData = new float[hunters.Length];
            for (int i = 0; i < hunters.Length; i++)
            {
                distanceData[i] = Vector3.Distance(localPosition, hunters[i].Position);
            }

            float minDistance = distanceData.Min();
            int index = Array.IndexOf(distanceData, minDistance);
            return hunters[index];
        }
    }
}
