namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Drawing;
    using MurderousPursuitHack.Input;
    using MurderousPursuitHack.Misc;

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
            Builder.StartSection("MISC");
            if (Builder.Button(DrawingHelper.DisplayKeybind("Auto Kill", InputManager.Instance.Keybindings.AutoKill)))
            {
                AutoKill.Instance.enabled = !AutoKill.Instance.enabled;
            }
            
            Builder.EndSection();
        }
    }
}
