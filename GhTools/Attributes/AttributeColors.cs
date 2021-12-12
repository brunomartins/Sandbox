using Grasshopper.GUI.Canvas;
using System.Drawing;

namespace GhTools.Attributes
{
    public static class AttributeColors
    {
        public static GH_PaletteStyle StandardStyle => new GH_PaletteStyle(Color.FromArgb((int)byte.MaxValue, 200, 200, 200), Color.FromArgb((int)byte.MaxValue, 50, 50, 50), Color.FromArgb((int)byte.MaxValue, 0, 0, 0));

        public static GH_PaletteStyle MMColorStyle => new GH_PaletteStyle(Color.FromArgb((int)byte.MaxValue, 231, 47, 135), Color.Black, Color.FromArgb((int)byte.MaxValue, 38, 183, 188));
    }
}