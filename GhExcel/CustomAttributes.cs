using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using System.Drawing;

namespace GhExcel
{
    // https://discourse.mcneel.com/t/custome-node-color/7427/6
    // https://discourse.mcneel.com/t/change-the-color-of-the-custom-component/56435/2
    internal class CustomAttributes : GH_ComponentAttributes
    {
        public CustomAttributes(IGH_Component component) : base(component)
        {
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            if (channel == GH_CanvasChannel.Objects)
            {
                GH_PaletteStyle standardStyle = new GH_PaletteStyle(Color.FromArgb((int)byte.MaxValue, 200, 200, 200), Color.FromArgb((int)byte.MaxValue, 50, 50, 50), Color.FromArgb((int)byte.MaxValue, 0, 0, 0));
                GH_Skin.palette_normal_standard = new GH_PaletteStyle(Color.DeepPink, Color.Black, Color.PapayaWhip);
                base.Render(canvas, graphics, channel);
                GH_Skin.palette_normal_standard = standardStyle;
            }
            else
                base.Render(canvas, graphics, channel);
        }
    }
}
