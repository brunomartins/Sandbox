using System.Drawing;
using Grasshopper.GUI.Canvas;

namespace SandboxGh.Attributes
{
    internal static class AttributeColors
    {
        internal static Color PinkMM => Color.FromArgb((int) byte.MaxValue, 231, 47, 135);

        internal static Color AquaMM => Color.FromArgb((int)byte.MaxValue, 38, 183, 188);

        internal static Pen PenMM => new Pen(new SolidBrush(PinkMM), 6);

        internal static GH_PaletteStyle StandardStyle => new GH_PaletteStyle(Color.FromArgb((int)byte.MaxValue, 200, 200, 200), Color.FromArgb((int)byte.MaxValue, 50, 50, 50), Color.FromArgb((int)byte.MaxValue, 0, 0, 0));

        internal static GH_PaletteStyle MMColorStyle => new GH_PaletteStyle(PinkMM, AquaMM, Color.Black);
    }
}