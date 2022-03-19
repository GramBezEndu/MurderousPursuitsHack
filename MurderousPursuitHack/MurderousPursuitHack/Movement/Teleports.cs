namespace MurderousPursuitHack.Movement
{
    using MurderousPursuitHack.Managers;
    using MurderousPursuitHack.Players;
    using System;
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

            if (HackManager.Instance.QuarryId == HackManager.InvalidPlayerId)
            {
                return false;
            }

            PlayerData target = PlayersHelper.SafeGetQuarry();

            if (target == null)
            {
                return false;
            }

            return TeleportLocalToPlayer(target, Settings.Current.AutoAttackAfterTeleport);
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

            return TeleportLocalToPlayer(closestHunter, Settings.Current.AutoAttackAfterTeleport);
        }

        public static bool TeleportQuarry()
        {
            if (!HackManager.Instance.InGame)
            {
                return false;
            }

            PlayerData quarry = PlayersHelper.SafeGetQuarry();
            if (quarry == null)
            {
                return false;
            }

            return TeleportPlayerToLocal(quarry, Settings.Current.AutoAttackAfterTeleport);
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

            return TeleportPlayerToLocal(closestHunter, Settings.Current.AutoAttackAfterTeleport);
        }

        public static bool TeleportPlayerToLocal(PlayerData from, bool attackAutomatically)
        {
            if (TeleportPlayer(from, PlayersHelper.SafeGetLocalPlayer().Transform.position))
            {
                if (attackAutomatically)
                {
                    return Managers.AbilityManager.Instance.StartAttack(from.Player);
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
            if (TeleportLocalPlayer(to.Transform.position))
            {
                if (attackAutomatically)
                {
                    return Managers.AbilityManager.Instance.StartAttack(to.Player);
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
            return TeleportPlayer(PlayersHelper.SafeGetLocalPlayer(), position);
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

        public static PlayerData GetClosestHunter()
        {
            PlayerData[] hunters = HackManager.Instance.AliveHunters.ToArray();
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
                distanceData[i] = Vector3.Distance(localPosition, hunters[i].Transform.position);
            }

            float minDistance = distanceData.Min();
            int index = Array.IndexOf(distanceData, minDistance);
            return hunters[index];
        }
    }
}
