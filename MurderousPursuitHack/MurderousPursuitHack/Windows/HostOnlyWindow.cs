namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Drawing;
    using MurderousPursuitHack.Input;
    using MurderousPursuitHack.Managers;
    using ProjectX.Abilities;

    public class HostOnlyWindow : Window
    {
        public static HostOnlyWindow Instance { get; private set; }

        public override void Start()
        {
            base.Start();
            Name = "HOST ONLY";
            Instance = this;
        }

        protected override void CreateElements(int windowID)
        {
            bool isHosting = HackManager.Instance.IsHost;
            Builder.Start();
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
        }
    }
}
