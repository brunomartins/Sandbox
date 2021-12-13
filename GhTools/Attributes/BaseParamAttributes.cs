using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using System.Drawing;

namespace GhTools.Attributes
{
    internal class BaseParamAttributes : GH_FloatingParamAttributes
    {
        public BaseParamAttributes(IGH_Param param) : base(param)
        {
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            if (channel == GH_CanvasChannel.Objects)
            {
                GH_Skin.palette_normal_standard = AttributeColors.MMColorStyle;
                base.Render(canvas, graphics, channel);
                GH_Skin.palette_normal_standard = AttributeColors.StandardStyle;
            }
            else
                base.Render(canvas, graphics, channel);
        }
    }
}
