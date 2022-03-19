namespace MurderousPursuitHack.Players
{
    using MurderousPursuitHack.Managers;

    public static class PlayersHelper
    {
        public static PlayerData SafeGetQuarry()
        {
            if (HackManager.Instance.QuarryId == HackManager.InvalidPlayerId)
            {
                return null;
            }
            else
            {
                return HackManager.Instance.Players[HackManager.Instance.QuarryId];
            }
        }

        public static PlayerData SafeGetLocalPlayer()
        {
            if (HackManager.Instance.LocalPlayerId == HackManager.InvalidPlayerId)
            {
                return null;
            }
            else
            {
                return HackManager.Instance.Players[HackManager.Instance.LocalPlayerId];
            }
        }
    }
}
