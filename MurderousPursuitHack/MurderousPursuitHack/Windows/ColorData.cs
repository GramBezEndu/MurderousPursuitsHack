using System;
using UnityEngine;

namespace MurderousPursuitHack.Windows
{

    public class ColorData
    {
        private Color32 color;

        public ColorData() 
        {
            Color = new Color32(255, 255, 255, 255);
        }

        public ColorData(Color color)
        {
            Color = color;
        }

        public event EventHandler OnColorChanged;

        public string Description { get; set; } = string.Empty;

        public Color32 Color
        {
            get
            {
                return color;
            }
            set
            {
                if (color.r != value.r || color.g != value.g || color.b != value.b || color.a != value.a)
                {
                    color = value;
                    OnColorChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public byte R
        {
            get => color.r;
            set
            {
                if (color.r != value)
                {
                    color.r = value;
                    OnColorChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public byte G
        {
            get => color.g;
            set
            {
                if (color.g != value)
                {
                    color.g = value;
                    OnColorChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public byte B
        {
            get => color.b;
            set
            {
                if (color.b != value)
                {
                    color.b = value;
                    OnColorChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public byte A
        {
            get => color.a;
            set
            {
                if (color.a != value)
                {
                    color.a = value;
                    OnColorChanged?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
