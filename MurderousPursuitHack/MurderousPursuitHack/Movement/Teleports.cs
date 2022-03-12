namespace MurderousPursuitHack.Movement
{
    using MurderousPursuitHack.Managers;
    using UnityEngine;

    public static class Teleports
    {
        public static bool TeleportToQuarry()
        {
            if (HackManager.Instance == null)
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

        public static bool TeleportToHunter()
        {
            if (HackManager.Instance == null)
            {
                return false;
            }

            PlayerData hunter = HackManager.Instance.Players.Find(x => x.IsHunterForLocal);
            if (hunter == null)
            {
                return false;
            }

            return TeleportLocalToPlayer(hunter, Settings.AutoAttackAfterTeleport);
        }

        public static bool TeleportQuarry()
        {
            PlayerData quarry = HackManager.Instance.Players.Find(x => x.IsQuarryForLocal);
            if (quarry == null)
            {
                return false;
            }

            return TeleportPlayerToLocal(quarry, Settings.AutoAttackAfterTeleport);
        }

        public static bool TeleportHunter()
        {
            PlayerData hunter = HackManager.Instance.Players.Find(x => x.IsHunterForLocal);
            if (hunter == null)
            {
                return false;
            }

            return TeleportPlayerToLocal(hunter, Settings.AutoAttackAfterTeleport);
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
            if (HackManager.Instance == null)
            {
                return false;
            }

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
    }
}
