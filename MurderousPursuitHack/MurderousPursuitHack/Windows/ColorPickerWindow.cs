namespace MurderousPursuitHack.Windows
{
    public class ColorPickerWindow : Window
    {
        public static ColorPickerWindow Instance { get; private set; }

        public override void Start()
        {
            base.Start();
            Name = "COLOR PICKER";
            Instance = this;
        }

        public ColorData ColorData { get; set; }

        protected override void CreateElements(int windowID)
        {
            Builder.Start();
            BuildColorPicker(ColorData);
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
