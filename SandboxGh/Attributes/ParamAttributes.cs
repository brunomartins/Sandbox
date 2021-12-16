using System.Drawing;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;

namespace SandboxGh.Attributes
{
    internal class ParamAttributes : GH_FloatingParamAttributes
    {
        internal ParamAttributes(IGH_Param param) : base(param)
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
