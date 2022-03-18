namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Drawing;
    using MurderousPursuitHack.Input;
    using MurderousPursuitHack.Visuals;

    public class VisualsWindow : Window
    {
        public static VisualsWindow Instance { get; private set; }

        public ColorPreview LocalGlowColor { get; private set; }

        public ColorPreview QuarryGlowColor { get; private set; }

        public ColorPreview HunterGlowColor { get; private set; }

        public ColorPreview NeutralGlowColor { get; private set; }

        public override void Start()
        {
            base.Start();
            LocalGlowColor = new ColorPreview(Settings.Current.LocalChamsColor);
            QuarryGlowColor = new ColorPreview(Settings.Current.QuarryChams);
            HunterGlowColor = new ColorPreview(Settings.Current.HunterChams);
            NeutralGlowColor = new ColorPreview(Settings.Current.NeutralChams);
            Name = "VISUALS";
            Instance = this;
        }

        protected override void CreateElements(int windowID)
        {
            Builder.Start();
            Builder.StartSection("PLAYER GLOW", 175f);
            Settings.Current.DrawLocalPlayerChams =
                Builder.Toggle(Settings.Current.DrawLocalPlayerChams, DrawingHelper.DisplayKeybind("Local Player", InputManager.Instance.Keybindings.LocalPlayerChams));
            Builder.ColorPreview(LocalGlowColor);
            Builder.Toggle(true, "Quarry");
            Builder.ColorPreview(QuarryGlowColor);
            Builder.Toggle(true, "Hunter");
            Builder.ColorPreview(HunterGlowColor);
            Builder.Toggle(true, "Neutral");
            Builder.ColorPreview(NeutralGlowColor);
            Builder.EndSection();

            Builder.StartSection("PLAYER ESP", 70f);
            Settings.Current.EspEnabled = Builder.Toggle(Settings.Current.EspEnabled, DrawingHelper.DisplayKeybind("Player ESP", InputManager.Instance.Keybindings.PlayerESP));
            Builder.EndSection();

            Builder.StartSection("SKINS", 72f);
            if (Builder.Button(DrawingHelper.DisplayKeybind("Random skin", InputManager.Instance.Keybindings.ChangeSkin)))
            {
                Skins.ChangeLocalPlayerSkin();
            }
            Builder.EndSection();

            Builder.StartSection("ANIMATIONS", 72f);
            if (Builder.Button(DrawingHelper.DisplayKeybind("Animation freeze", InputManager.Instance.Keybindings.FreezeAnimation)))
            {
                Animations.ToggleAnimationFreeze();
            }
            Builder.EndSection();
        }
    }
}
