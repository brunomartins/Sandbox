using System.Drawing;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;

namespace GhTools.Attributes
{
    // https://discourse.mcneel.com/t/custome-node-color/7427/6
    // https://discourse.mcneel.com/t/change-the-color-of-the-custom-component/56435/2
    internal class BaseCustomAttribute : GH_ComponentAttributes
    {
        public BaseCustomAttribute(IGH_Component component) : base(component)
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
