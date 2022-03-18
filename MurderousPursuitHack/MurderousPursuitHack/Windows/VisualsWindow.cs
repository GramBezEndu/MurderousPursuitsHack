namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Drawing;
    using MurderousPursuitHack.Input;
    using MurderousPursuitHack.Visuals;

    public class VisualsWindow : Window
    {
        private bool initialized;

        public static VisualsWindow Instance { get; private set; }

        public ColorPreview LocalGlowColor { get; private set; }

        public ColorPreview QuarryGlowColor { get; private set; }

        public ColorPreview HunterGlowColor { get; private set; }

        public ColorPreview NeutralGlowColor { get; private set; }

        public override void Start()
        {
            base.Start();
            LocalGlowColor = new ColorPreview(Settings.Current.LocalGlow);
            QuarryGlowColor = new ColorPreview(Settings.Current.QuarryGlow);
            HunterGlowColor = new ColorPreview(Settings.Current.HunterGlow);
            NeutralGlowColor = new ColorPreview(Settings.Current.NeutralGlow);
            Name = "VISUALS";
            Instance = this;
        }

        protected override void CreateElements(int windowID)
        {
            if (!initialized)
            {
                LocalGlowColor.Initialize();
                QuarryGlowColor.Initialize();
                HunterGlowColor.Initialize();
                NeutralGlowColor.Initialize();
                initialized = true;
            }

            Builder.Start();
            Builder.StartSection("PLAYER GLOW", 175f);
            Settings.Current.LocalChams =
                Builder.Toggle(Settings.Current.LocalChams, DrawingHelper.DisplayKeybind("Local Player", InputManager.Instance.Keybindings.LocalPlayerChams));
            if (Builder.ColorPreview(LocalGlowColor))
            {
                ManageColorPickerContext(LocalGlowColor);
            }

            Builder.Toggle(true, "Quarry");
            if (Builder.ColorPreview(QuarryGlowColor))
            {
                ManageColorPickerContext(QuarryGlowColor);
            }

            Builder.Toggle(true, "Hunter");
            if (Builder.ColorPreview(HunterGlowColor))
            {
                ManageColorPickerContext(HunterGlowColor);
            }
            Builder.Toggle(true, "Neutral");
            if (Builder.ColorPreview(NeutralGlowColor))
            {
                ManageColorPickerContext(NeutralGlowColor);
            }
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

        private void ManageColorPickerContext(ColorPreview preview)
        {
            if (ColorPickerWindow.Instance.ColorData == preview.ColorData)
            {
                ColorPickerWindow.Instance.SetContext(null);
            }
            else
            {
                ColorPickerWindow.Instance.SetContext(preview.ColorData);
            }
        }
    }
}
