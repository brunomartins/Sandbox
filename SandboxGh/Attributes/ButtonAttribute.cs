using System.Drawing;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using SandboxGh.Utility;

namespace SandboxGh.Attributes
{
    internal class ButtonAttribute : GH_ComponentAttributes
    {
        public ButtonAttribute(IGH_Component component) : base(component)
        {
        }

        private Rectangle ButtonBounds { get; set; }

        protected override void Layout()
        {
            base.Layout();

            var rec0 = GH_Convert.ToRectangle(Bounds);
            rec0.Height += 22;

            var rec1 = rec0;
            rec1.Y = rec1.Bottom - 22;
            rec1.Height = 22;
            rec1.Inflate(-2, -2);

            base.Bounds = rec0;
            ButtonBounds = rec1;
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            if (channel == GH_CanvasChannel.Objects)
            {
                graphics.DrawRectangle(ColorAttributes.SandboxPen, Rectangle.Round(Bounds));
            }
            base.Render(canvas, graphics, channel);
            var button = GH_Capsule.CreateTextCapsule(ButtonBounds, ButtonBounds, GH_Palette.Black, "ValueList", 2, 0);
            button.Render(graphics, Selected, Owner.Locked, false);
            button.Dispose();
        }

        public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return base.RespondToMouseDown(sender, e);
            if (!(Owner is CreateDictionary component)) return GH_ObjectResponse.Handled;
            RectangleF rec = ButtonBounds;
            if (!rec.Contains(e.CanvasLocation)) return base.RespondToMouseDown(sender, e);
            component.CreateValueList(sender, e);
            component.ExpireSolution(true);

            return GH_ObjectResponse.Handled;
        }
    }
}
