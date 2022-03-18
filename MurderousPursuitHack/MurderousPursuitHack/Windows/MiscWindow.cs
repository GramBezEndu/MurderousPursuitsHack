namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Drawing;
    using MurderousPursuitHack.Input;
    using MurderousPursuitHack.Managers;
    using MurderousPursuitHack.Misc;
    using ProjectX.Abilities;

    public class MiscWindow : Window
    {
        public static MiscWindow Instance { get; private set; }

        public override void Start()
        {
            base.Start();
            Name = "MISC";
            Instance = this;
        }

        protected override void CreateElements(int windowID)
        {
            Builder.Start();
            RageSection();
            HostOnlySection();
        }

        private void RageSection()
        {
            Builder.StartSection("RAGE", 75f);
            if (Builder.Button(DrawingHelper.DisplayKeybind("Auto Kill", InputManager.Instance.Keybindings.AutoKill)))
            {
                AutoKill.Instance.enabled = !AutoKill.Instance.enabled;
            }

            Builder.EndSection();
        }

        private void HostOnlySection()
        {
            Builder.StartSection("HOST ONLY", 185f);
            bool isHosting = HackManager.Instance.IsHost;
            Settings.Current.ZeroExposure = Builder.Toggle(Settings.Current.ZeroExposure, DrawingHelper.DisplayKeybind("Zero Exposure", InputManager.Instance.Keybindings.ZeroExposure));
            if (!isHosting)
            {
                Builder.StartDisabled();
            }

            if (Builder.Button(DrawingHelper.DisplayKeybind("Pie Bomb", InputManager.Instance.Keybindings.PieBomb)))
            {
                Managers.AbilityManager.Instance.StartAbility<XPlacePieBomb>();
            }

            if (Builder.Button(DrawingHelper.DisplayKeybind("Flash", InputManager.Instance.Keybindings.Flash)))
            {
                Managers.AbilityManager.Instance.StartAbility<XFlash>();
            }

            if (Builder.Button(DrawingHelper.DisplayKeybind("Disrupt", InputManager.Instance.Keybindings.Disrupt)))
            {
                Managers.AbilityManager.Instance.StartAbility<XDisrupt>();
            }

            if (!isHosting)
            {
                Builder.EndDisabled();
            }

            Builder.EndSection();
        }
    }
}
