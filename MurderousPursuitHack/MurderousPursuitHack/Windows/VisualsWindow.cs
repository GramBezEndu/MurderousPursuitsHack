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

        public ColorPreview QuarryEsp { get; private set; }

        public ColorPreview HunterEsp { get; private set; }

        public ColorPreview NeutralEsp { get; private set; }

        public override void Awake()
        {
            base.Awake();
            Name = "VISUALS";
            Instance = this;
        }

        public override void Start()
        {
            base.Start();
            Settings settings = Settings.Current;
            LocalGlowColor = new ColorPreview(settings.LocalGlow);
            QuarryGlowColor = new ColorPreview(settings.QuarryGlow);
            HunterGlowColor = new ColorPreview(settings.HunterGlow);
            NeutralGlowColor = new ColorPreview(settings.NeutralGlow);

            QuarryEsp = new ColorPreview(settings.QuarryEspColor);
            HunterEsp = new ColorPreview(settings.HunterEspColor);
            NeutralEsp = new ColorPreview(settings.NeutralEspColor);
        }

        protected override void CreateElements(int windowID)
        {
            if (!initialized)
            {
                LocalGlowColor.Initialize();
                QuarryGlowColor.Initialize();
                HunterGlowColor.Initialize();
                NeutralGlowColor.Initialize();
                QuarryEsp.Initialize();
                HunterEsp.Initialize();
                NeutralEsp.Initialize();
                initialized = true;
            }

            Settings settings = Settings.Current;

            Builder.Start();
            ChamsSection(settings);
            EspSection(settings);
            SkinsSection();
            AnimationsSection();
        }

        private void AnimationsSection()
        {
            Builder.StartSection("ANIMATIONS", 72f);
            if (Builder.Button(DrawingHelper.DisplayKeybind("Animation freeze", InputManager.Instance.Keybindings.FreezeAnimation)))
            {
                Animations.ToggleAnimationFreeze();
            }
            Builder.EndSection();
        }

        private void SkinsSection()
        {
            Builder.StartSection("SKINS", 72f);
            if (Builder.Button(DrawingHelper.DisplayKeybind("Random skin", InputManager.Instance.Keybindings.ChangeSkin)))
            {
                Skins.ChangeLocalPlayerSkin();
            }
            Builder.EndSection();
        }

        private void EspSection(Settings settings)
        {
            Builder.StartSection("PLAYER ESP", 140f);
            settings.QuarryEsp = Builder.Toggle(settings.QuarryEsp, "Quarry");
            if (Builder.ColorPreview(QuarryEsp))
            {
                ManageColorPickerContext(QuarryEsp);
            }

            settings.HunterEsp = Builder.Toggle(settings.HunterEsp, "Hunter");
            if (Builder.ColorPreview(HunterEsp))
            {
                ManageColorPickerContext(HunterEsp);
            }

            settings.NeutralEsp = Builder.Toggle(settings.NeutralEsp, "Neutral");
            if (Builder.ColorPreview(NeutralEsp))
            {
                ManageColorPickerContext(NeutralEsp);
            }

            Builder.EndSection();
        }

        private void ChamsSection(Settings settings)
        {
            Builder.StartSection("PLAYER GLOW", 175f);
            settings.LocalChams = Builder.Toggle(settings.LocalChams, "Local Player");
            if (Builder.ColorPreview(LocalGlowColor))
            {
                ManageColorPickerContext(LocalGlowColor);
            }

            settings.QuarryChams = Builder.Toggle(settings.QuarryChams, "Quarry");
            if (Builder.ColorPreview(QuarryGlowColor))
            {
                ManageColorPickerContext(QuarryGlowColor);
            }

            settings.HunterChams = Builder.Toggle(settings.HunterChams, "Hunter");
            if (Builder.ColorPreview(HunterGlowColor))
            {
                ManageColorPickerContext(HunterGlowColor);
            }
            settings.NeutralChams = Builder.Toggle(settings.NeutralChams, "Neutral");
            if (Builder.ColorPreview(NeutralGlowColor))
            {
                ManageColorPickerContext(NeutralGlowColor);
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
