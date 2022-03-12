namespace MurderousPursuitHack
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class TeleportManager
    {
        public static void TeleportToQuarry()
        {
            if (GameInfoManager.Instance != null)
            {
                PlayerData target = GameInfoManager.Instance.Players.Find(x => x.IsQuarryForLocal);
                if (target != null)
                {
                    TeleportLocalPlayer(target.Position);
                }
            }
        }

        public static void TeleportToAnyHunter()
        {
            if (GameInfoManager.Instance != null)
            {
                List<PlayerData> hunters = GameInfoManager.Instance.Players.FindAll(x => x.IsHunterForLocal);
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

        public static void TeleportQuarryToLocal()
        {
            if (GameInfoManager.Instance != null)
            {
                if (GameInfoManager.Instance.LocalPlayer == null)
                {
                    return;
                }
                Vector3 position = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer).Position;
                TeleportQuarry(position);
            }
        }

        private static void TeleportQuarry(Vector3 position)
        {
            PlayerData quarry = GameInfoManager.Instance.Players.Find(x => x.IsQuarryForLocal);
            if (quarry != null)
            {
                TeleportPlayer(quarry, position);
            }
        }

        public static void TeleportHuntersToLocal()
        {
            if (GameInfoManager.Instance == null)
            {
                return;
            }

            if (GameInfoManager.Instance.LocalPlayer == null)
            {
                return;
            }

            Vector3 position = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer).Position;
            TeleportQuarry(position);
            List<PlayerData> hunters = GameInfoManager.Instance.Players.FindAll(x => x.IsHunterForLocal);
            if (hunters != null)
            {
                foreach (PlayerData hunter in hunters)
                {
                    if (hunter.IsAlive)
                    {
                        TeleportPlayer(hunter, position);
                    }
                }
            }
        }

        private static void TeleportLocalPlayer(Vector3 position)
        {
            TeleportPlayer(GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer), position);
        }

        private static void TeleportPlayer(PlayerData playerData, Vector3 position)
        {
            if (GameInfoManager.Instance != null)
            {
                if (playerData != null)
                {
                    Transform characterTransform = (Transform)(playerData.CharacterMovement.GetFieldValue("characterTransform"));
                    if (characterTransform != null)
                    {
                        characterTransform.position = position;
                    }
                }
            }
        }
    }
}
