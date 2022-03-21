namespace MurderousPursuitHack.Players
{
    using System;

    public class PlayerArgs : EventArgs
    {
        public PlayerArgs(PlayerData player)
        {
            PlayerData = player;
        }

        public PlayerData PlayerData { get; private set; }
    }
}
