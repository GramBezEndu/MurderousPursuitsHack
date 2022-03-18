using UnityEngine;

namespace MurderousPursuitHack.Windows
{
    public class ColorsWindow : Window
    {
        public static ColorsWindow Instance { get; private set; }

        public Vector2 scrollPosition = Vector2.zero;

        public override void Start()
        {
            base.Start();
            Name = "COLORS";
            Instance = this;
        }

        protected override void CreateElements(int windowID)
        {
            Builder.Start();
            Builder.StartSection("CHAMS", 350f);
            scrollPosition = Builder.StartScrollView(scrollPosition, new Vector2(Size.x, 600f));
            LocalPlayerChams();
            QuarryChams();
            HunterChams();
            Builder.EndScrollView();
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

        private void HunterChams()
        {
            Builder.Label("HUNTER");
            BuildColorPicker(Settings.Current.HunterChams);
        }

        private void BuildColorPicker(ColorData colorData)
        {
            float cachedMargin = ElementsMarginY;
            ElementsMarginY = 1f;
            colorData.R = Builder.LabelSlider("R", colorData.R, 0, 255, Builder.Styles.RedSlider);
            colorData.G = Builder.LabelSlider("G", colorData.G, 0, 255, Builder.Styles.GreenSlider);
            colorData.B = Builder.LabelSlider("B", colorData.B, 0, 255, Builder.Styles.BlueSlider);
            colorData.A = Builder.LabelSlider("A", colorData.A, 0, 255, Builder.Styles.WhiteSlider);
            ElementsMarginY = cachedMargin;
        }
    }
}
