using System.Drawing;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;

namespace SandboxGh.Attributes
{
    // https://discourse.mcneel.com/t/custome-node-color/7427/6
    // https://discourse.mcneel.com/t/change-the-color-of-the-custom-component/56435/2
    internal class BaseComponentAttribute : GH_ComponentAttributes
    {
        public BaseComponentAttribute(IGH_Component component) : base(component)
        {
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            if (channel == GH_CanvasChannel.Objects)
            {
                graphics.DrawRectangle(AttributeColors.PenMM, Rectangle.Round(Bounds));
            }

            base.Render(canvas, graphics, channel);
        }
    }
}
