namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Managers;
    using ProjectX.Networking;
    using System;
    using UnityEngine;

    public class DebugWindow : Window
    {
        public static DebugWindow Instance { get; private set; }

        public override void Start()
        {
            base.Start();
            Name = "DEBUG";
            Position = new Vector2(10f, 10f);
            Size = new Vector2(300f, 240f);
            Instance = this;
        }

        protected override void CreateElements(int windowID)
        {
            bool inGame = HackManager.Instance.InGame;
            Builder.Start();
            Builder.Label("Mono memory: " + Math.Round(System.GC.GetTotalMemory(false) / Math.Pow(10, 6), 1) + " MB");
            Builder.Label("In Game: " + inGame);
            if (inGame)
            {
                Builder.Label("Is Host: " + HackManager.Instance.IsHost.ToString());
                Builder.Label("Game Type: " + UNetManager.Instance.GameType);
                Builder.Label("Quarry Bar found: " + ((HackManager.Instance.QuarryBar != null) ? "True" : "False"));
                Builder.Label("Hunter HUD found: " + ((HackManager.Instance.HunterHUD != null) ? "True" : "False"));
            }
        }
    }
}
