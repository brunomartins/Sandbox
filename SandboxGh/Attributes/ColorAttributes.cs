using System.Drawing;
using Grasshopper.GUI.Canvas;

namespace SandboxGh.Attributes
{
    internal static class ColorAttributes
    {
        internal static Color SandboxPink => Color.FromArgb((int) byte.MaxValue, 231, 47, 135);

        internal static Color SandboxAqua => Color.FromArgb((int)byte.MaxValue, 38, 183, 188);

        internal static Pen SandboxPen => new Pen(new SolidBrush(SandboxPink), 6);

        internal static GH_PaletteStyle StandardStyle => new GH_PaletteStyle(Color.FromArgb((int)byte.MaxValue, 200, 200, 200), Color.FromArgb((int)byte.MaxValue, 50, 50, 50), Color.FromArgb((int)byte.MaxValue, 0, 0, 0));

        internal static GH_PaletteStyle SandboxPaletteStyle => new GH_PaletteStyle(SandboxPink, SandboxAqua, Color.Black);
    }
}