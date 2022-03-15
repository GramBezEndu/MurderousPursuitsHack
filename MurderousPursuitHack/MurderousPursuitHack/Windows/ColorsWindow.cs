using UnityEngine;

namespace MurderousPursuitHack.Windows
{
    public class ColorsWindow : Window
    {
        public static ColorsWindow Instance { get; private set; }

        private Vector2 sliderScale = new Vector2(0.9f, 0.75f);

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
            QuarryChams();
            Builder.EndSection();
        }

        private void LocalPlayerChams()
        {
            Builder.Label("LOCAL PLAYER");  
            BuildColorPicker(Settings.Current.LocalChamsColor);
        }

        private void QuarryChams()
        {
            Builder.Label("QUARRY");
            BuildColorPicker(Settings.Current.QuarryChams);
        }

        private void BuildColorPicker(ColorData colorData)
        {
            float cachedMargin = ElementsMarginY;
            ElementsMarginY = 3f;
            colorData.R = Builder.LabelSlider("R", colorData.R, 0, 255, Builder.Style.RedSlider);
            colorData.G = Builder.LabelSlider("G", colorData.G, 0, 255, Builder.Style.GreenSlider);
            colorData.B = Builder.LabelSlider("B", colorData.B, 0, 255, Builder.Style.BlueSlider);
            colorData.A = Builder.LabelSlider("A", colorData.A, 0, 255, Builder.Style.WhiteSlider);
            ElementsMarginY = cachedMargin;
        }
    }
}
