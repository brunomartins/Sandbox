using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using SandboxCore.Utilities;
using System.Drawing;

namespace SandboxGh.Attributes
{
    // https://discourse.mcneel.com/t/custome-node-color/7427/6
    // https://discourse.mcneel.com/t/change-the-color-of-the-custom-component/56435/2
    internal class ComponentAttribute : GH_ComponentAttributes
    {
        internal ComponentAttribute(IGH_Component component) : base(component)
        {
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            if (channel == GH_CanvasChannel.Objects)
            {
                graphics.DrawRectangle(ColorAttributes.SandboxPen, Rectangle.Round(Bounds));
            }
            base.Render(canvas, graphics, channel);
        }
    }
}
