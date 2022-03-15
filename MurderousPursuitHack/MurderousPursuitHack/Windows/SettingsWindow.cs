namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Drawing;
    using System;
    using UnityEngine;

    public class SettingsWindow : Window
    {
        private Window activeWindow;

        public static SettingsWindow Instance { get; private set; }

        public Window ActiveWindow 
        { 
            get => activeWindow; 
            private set
            {
                if (activeWindow != value)
                {
                    activeWindow = value;
                    HideAllWindows();
                    activeWindow.Visible = true;
                }
            }
        }

        public override void Start()
        {
            base.Start();
            Name = string.Empty;
            Position = new Vector2(10f, 260f);
            Size = new Vector2(160, 360);
            ElementHeight = 45f;
            ElementsMarginY = 25f;
            Visible = Settings.CheatsWindow;
            OnVisibleChanged += (o, e) => OnVisibilityChanged();
            Instance = this;
        }

        protected override void CreateElements(int windowID)
        {
            if (ActiveWindow == null)
            {
                ActiveWindow = VisualsWindow.Instance;
            }

            Builder.Start();
            Builder.CurrentElementPosition = new Vector2(0f, 50f);
            if (VisualsSection(Builder))
            {
                ActiveWindow = VisualsWindow.Instance;
            }

            if (MovementSection(Builder))
            {
                ActiveWindow = MovementWindow.Instance;
            }

            if (HostOnlySection(Builder))
            {
                ActiveWindow = HostOnlyWindow.Instance;
            }

            if (DebugSection(Builder))
            {
                DebugWindow.Instance.Visible = !DebugWindow.Instance.Visible;
            }
        }

        private bool VisualsSection(WindowBuilder builder)
        {
            return builder.Button("VISUALS");
        }

        private bool DebugSection(WindowBuilder builder)
        {
            return builder.Button("DEBUG");
        }

        private bool MovementSection(WindowBuilder builder)
        {
            return builder.Button("MOVEMENT");
        }

        private bool HostOnlySection(WindowBuilder builder)
        {
            return builder.Button("HOST ONLY");
        }

        private void OnVisibilityChanged()
        {
            if (ActiveWindow == null)
            {
                return;
            }

            if (Visible)
            {
                ActiveWindow.Visible = true;
            }
            else
            {
                ActiveWindow.Visible = false;
            }
        }

        private void HideAllWindows()
        {
            VisualsWindow.Instance.Visible = false;
            MovementWindow.Instance.Visible = false;
            HostOnlyWindow.Instance.Visible = false;
        }
    }
}
