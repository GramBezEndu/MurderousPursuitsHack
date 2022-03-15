using System;
using UnityEngine;

namespace MurderousPursuitHack.Windows
{

    public class ColorData
    {
        private int r = 255;
        
        private int g = 255;
        
        private int b = 255;

        private int a = 255;

        public event EventHandler OnColorChanged;

        private Color color = Color.white;

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                if (color != value)
                {
                    color = value;
                    OnColorChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public int R 
        {
            get => r;
            set
            {
                if (r != value)
                {
                    r = value;
                    Color = UpdateColor();
                }
            }
        }

        public int G
        {
            get => g;
            set
            {
                if (g != value)
                {
                    g = value;
                    Color = UpdateColor();
                }
            }
        }

        public int B
        {
            get => b;
            set
            {
                if (b != value)
                {
                    b = value;
                    Color = UpdateColor();
                }
            }
        }

        public int A
        {
            get => a;
            set
            {
                if (a != value)
                {
                    a = value;
                    Color = UpdateColor();
                }
            }
        }

        private Color UpdateColor()
        {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }
    }
}
