namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Drawing;
    using System.Collections.Generic;
    using UnityEngine;

    public class SettingsWindow : Window
    {
        private Window activeWindow;

        public static SettingsWindow Instance { get; private set; }

        private List<Window> windows = new List<Window>();

        public Window ActiveWindow
        {
            get => activeWindow;
            private set
            {
                if (activeWindow != value)
                {
                    activeWindow = value;
                    ColorPickerWindow.Instance.SetContext(null);
                    ShowActiveWindow();
                }
            }
        }

        public override void Awake()
        {
            base.Awake();
            Name = string.Empty;
            Position = new Vector2(10f, 260f);
            Size = new Vector2(160, 530);
            ElementHeight = 45f;
            ElementsMarginY = 30f;
            Instance = this;
        }

        public override void Start()
        {
            base.Start();
            Visible = Settings.Current.CheatsWindow;
            windows = new List<Window>()
            {
                VisualsWindow.Instance,
                MovementWindow.Instance,
                MiscWindow.Instance,
                Config.Instance,
            };
            OnVisibleChanged += (o, e) => OnVisibilityChanged();
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

            if (MiscSection(Builder))
            {
                ActiveWindow = MiscWindow.Instance;
            }

            if (ConfigSection(Builder))
            {
                ActiveWindow = Config.Instance;
            }

            if (DebugSection(Builder))
            {
                DebugWindow.Instance.Visible = !DebugWindow.Instance.Visible;
            }
        }

        private bool MiscSection(WindowBuilder builder)
        {
            return builder.Button("MISC");
        }

        private bool VisualsSection(WindowBuilder builder)
        {
            return builder.Button("VISUALS");
        }

        private bool MovementSection(WindowBuilder builder)
        {
            return builder.Button("MOVEMENT");
        }

        private bool ConfigSection(WindowBuilder builder)
        {
            return builder.Button("CONFIG");
        }

        private bool DebugSection(WindowBuilder builder)
        {
            return builder.Button("DEBUG");
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
                ColorPickerWindow.Instance.SetContext(null);
            }
        }

        private void ShowActiveWindow()
        {
            foreach (Window window in windows)
            {
                if (window == activeWindow)
                {
                    window.Visible = true;
                }
                else
                {
                    window.Visible = false;
                }
            }
        }
    }
}
