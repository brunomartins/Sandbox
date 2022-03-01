using System.Drawing;

namespace SandboxCore.Utilities
{
    public static class ColorAttributes
    {
        public static Color SandboxPink => Color.FromArgb((int) byte.MaxValue, 231, 47, 135);

        public static Color SandboxAqua => Color.FromArgb((int)byte.MaxValue, 38, 183, 188);

        public static Pen SandboxPen => new Pen(new SolidBrush(SandboxPink), 6);
    }
}