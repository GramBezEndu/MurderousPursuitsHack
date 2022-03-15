namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Drawing;
    using MurderousPursuitHack.Input;
    using MurderousPursuitHack.Visuals;

    public class VisualsWindow : Window
    {
        public static VisualsWindow Instance { get; private set; }

        public override void Start()
        {
            base.Start();
            Name = "VISUALS";
            Instance = this;
        }

        protected override void CreateElements(int windowID)
        {
            Builder.Start();
            Builder.StartSection("CHAMS");
            Settings.ChamsEnabled = Builder.Toggle(Settings.ChamsEnabled, DrawingHelper.DisplayKeybind("Chams", InputManager.Instance.Keybindings.Chams));
            Settings.DrawLocalPlayerChams =
                Builder.Toggle(Settings.DrawLocalPlayerChams, DrawingHelper.DisplayKeybind("Local Player Chams", InputManager.Instance.Keybindings.LocalPlayerChams));
            Builder.EndSection();

            Builder.StartSection("ESP");
            Settings.EspEnabled = Builder.Toggle(Settings.EspEnabled, DrawingHelper.DisplayKeybind("Player ESP", InputManager.Instance.Keybindings.PlayerESP));
            Builder.EndSection();

            Builder.StartSection("SKINS");
            if (Builder.Button(DrawingHelper.DisplayKeybind("Change skin", InputManager.Instance.Keybindings.ChangeSkin)))
            {
                Skins.ChangeLocalPlayerSkin();
            }
            Builder.EndSection();
        }
    }
}
