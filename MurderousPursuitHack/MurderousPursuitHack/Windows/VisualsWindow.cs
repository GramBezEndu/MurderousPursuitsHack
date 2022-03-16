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
            Settings.Current.ChamsEnabled = Builder.Toggle(Settings.Current.ChamsEnabled, DrawingHelper.DisplayKeybind("Chams", InputManager.Instance.Keybindings.Chams));
            Settings.Current.DrawLocalPlayerChams =
                Builder.Toggle(Settings.Current.DrawLocalPlayerChams, DrawingHelper.DisplayKeybind("Local Player Chams", InputManager.Instance.Keybindings.LocalPlayerChams));
            Builder.EndSection();

            Builder.StartSection("ESP");
            Settings.Current.EspEnabled = Builder.Toggle(Settings.Current.EspEnabled, DrawingHelper.DisplayKeybind("Player ESP", InputManager.Instance.Keybindings.PlayerESP));
            Builder.EndSection();

            Builder.StartSection("SKINS");
            if (Builder.Button(DrawingHelper.DisplayKeybind("Change skin", InputManager.Instance.Keybindings.ChangeSkin)))
            {
                Skins.ChangeLocalPlayerSkin();
            }
            Builder.EndSection();

            Builder.StartSection("ANIMATIONS");
            if (Builder.Button(DrawingHelper.DisplayKeybind("Animation freeze", InputManager.Instance.Keybindings.FreezeAnimation)))
            {
                Animations.ToggleAnimationFreeze();
            }
            Builder.EndSection();
        }
    }
}
