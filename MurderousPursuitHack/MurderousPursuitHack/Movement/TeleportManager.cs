namespace MurderousPursuitHack
{
    using ProjectX.Player;
    using UnityEngine;

    public static class TeleportManager
    {
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

        private static void TeleportLocalPlayer(Vector3 pos)
        {
            if (GameInfoManager.Instance != null)
            {
                var local = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer);
                if (local != null)
                {
                    Transform characterTransform = (Transform)(typeof(XCharacterMovement).GetField("characterTransform", Utils.FieldGetFlags).GetValue(local.CharacterMovement));
                    characterTransform.position = pos;
                }
            }
        }
    }
}
