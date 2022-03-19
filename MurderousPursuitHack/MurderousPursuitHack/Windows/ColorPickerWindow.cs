using UnityEngine;

namespace MurderousPursuitHack.Windows
{
    public class ColorPickerWindow : Window
    {
        public static ColorPickerWindow Instance { get; private set; }

        public override void Awake()
        {
            base.Awake();
            Name = "COLOR";
            Position = new Vector2(470f, 260f);
            Size = new Vector2(300f, 200f);
            Visible = false;
            Instance = this;
        }

        public ColorData ColorData { get; private set; }

        public void SetContext(ColorData colorData)
        {
            if (colorData != ColorData)
            {
                ColorData = colorData;
                if (colorData == null)
                {
                    Visible = false;
                }
                else
                {
                    Visible = true;
                }
            }
        }

        protected override void CreateElements(int windowID)
        {
            Builder.Start();
            BuildColorPicker(ColorData);
        }

        private void BuildColorPicker(ColorData colorData)
        {
            Builder.StartSection(colorData.Description, 150f);
            float cachedMargin = ElementsMarginY;
            ElementsMarginY = 1f;
            colorData.R = (byte)Builder.LabelSlider("R", colorData.R, 0, 255, Builder.Styles.RedSlider);
            colorData.G = (byte)Builder.LabelSlider("G", colorData.G, 0, 255, Builder.Styles.GreenSlider);
            colorData.B = (byte)Builder.LabelSlider("B", colorData.B, 0, 255, Builder.Styles.BlueSlider);
            colorData.A = (byte)Builder.LabelSlider("A", colorData.A, 0, 255, Builder.Styles.WhiteSlider);
            ElementsMarginY = cachedMargin;
            Builder.EndSection();
        }
    }
}
