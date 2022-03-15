using UnityEngine;

namespace MurderousPursuitHack.Windows
{
    public class ColorsWindow : Window
    {
        public static ColorsWindow Instance { get; private set; }

        private readonly float sliderWidthMultiplier = 0.8f;

        public override void Start()
        {
            base.Start();
            Name = "COLORS";
            Instance = this;
        }

        protected override void CreateElements(int windowID)
        {
            Builder.Start();
            Builder.StartSection("CHAMS");
            LocalPlayerChams();
            Builder.EndSection();
        }

        private void LocalPlayerChams()
        {
            Builder.Label("LOCAL PLAYER");
            BuildColorPicker(Settings.Current.LocalChamsColor);
        }

        private void BuildColorPicker(ColorData colorData)
        {
            colorData.R = Builder.Slider(colorData.R, 0, 255, Builder.Style.RedSlider, sliderWidthMultiplier);
            colorData.G = Builder.Slider(colorData.G, 0, 255, Builder.Style.GreenSlider, sliderWidthMultiplier);
            colorData.B = Builder.Slider(colorData.B, 0, 255, Builder.Style.BlueSlider, sliderWidthMultiplier);
            colorData.A = Builder.Slider(colorData.A, 0, 255, Builder.Style.WhiteSlider, sliderWidthMultiplier);
        }
    }
}
