using System.Drawing;
using Grasshopper.GUI.Canvas;
using SandboxCore.Utilities;

namespace SandboxGh.Attributes
{
    internal static class GhColorAttributes
    {
        internal static GH_PaletteStyle StandardStyle => new GH_PaletteStyle(Color.FromArgb((int)byte.MaxValue, 200, 200, 200), Color.FromArgb((int)byte.MaxValue, 50, 50, 50), Color.FromArgb((int)byte.MaxValue, 0, 0, 0));

        internal static GH_PaletteStyle SandboxPaletteStyle => new GH_PaletteStyle(ColorAttributes.SandboxPink, ColorAttributes.SandboxAqua, Color.Black);
    }
}