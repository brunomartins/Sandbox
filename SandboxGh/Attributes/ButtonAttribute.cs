using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using System.Drawing;
using System.Windows.Forms;
using Grasshopper.Kernel.Undo;
using Grasshopper.Kernel.Undo.Actions;
using SandboxCore.Utilities;

namespace SandboxGh.Attributes
{
    internal abstract class ButtonAttribute : GH_ComponentAttributes
    {
        internal bool _mouseOver;
        internal bool _mouseDown;
        internal RectangleF _buttonArea;
        internal string _buttonText;
        internal RectangleF _textArea;

        public ButtonAttribute(IGH_Component component, string buttonText) : base(component)
        {
            _mouseOver = false;
            _mouseDown = false;
            _buttonText = buttonText;
        }

        protected override void Layout()
        {
            Bounds = RectangleF.Empty;
            base.Layout();
            RectangleF bounds = Bounds;
            double left = (double)bounds.Left;
            bounds = Bounds;
            double bottom = (double)bounds.Bottom;
            bounds = Bounds;
            double width = (double)bounds.Width;
            _textArea = _buttonArea = new RectangleF((float)left, (float)bottom, (float)width, 20f);
            Bounds = RectangleF.Union(Bounds, _buttonArea);
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);
            if (channel != GH_CanvasChannel.Objects) return;

            GH_PaletteStyle impliedStyle = GH_CapsuleRenderEngine.GetImpliedStyle(GH_Palette.Grey, Selected, Owner.Locked, true);
            GH_Capsule textCapsule = GH_Capsule.CreateTextCapsule(_buttonArea, _textArea, GH_Palette.Black, _buttonText, 1, 9);
            textCapsule.RenderEngine.RenderBackground(graphics, canvas.Viewport.Zoom, impliedStyle);
            
            graphics.DrawRectangle(ColorAttributes.SandboxPen, Rectangle.Round(Bounds));
            base.Render(canvas, graphics, channel);

            if (!_mouseDown)
            {
                textCapsule.RenderEngine.RenderHighlight(graphics);
            }
            textCapsule.RenderEngine.RenderOutlines(graphics, canvas.Viewport.Zoom, impliedStyle);

            if (_mouseOver)
            {
                textCapsule.RenderEngine.RenderBackground_Alternative(graphics, Color.FromArgb(50, ColorAttributes.SandboxAqua), false);
            }
            textCapsule.RenderEngine.RenderText(graphics, Color.White);
            textCapsule.Dispose();
        }

        public override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (!_buttonArea.Contains(e.CanvasLocation))
            {
                _mouseOver = false;
            }

            if (_mouseDown) return base.RespondToMouseUp(sender, e);

            _mouseDown = false;
            sender.Invalidate();
            return GH_ObjectResponse.Release;
        }

        public override GH_ObjectResponse RespondToMouseMove(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            Point point = GH_Convert.ToPoint(e.CanvasLocation);
            if (e.Button != MouseButtons.None) return base.RespondToMouseMove(sender, e);

            if (_buttonArea.Contains((PointF)point))
            {
                if (_mouseOver) return GH_ObjectResponse.Capture;
                _mouseOver = true;
                sender.Invalidate();
                return GH_ObjectResponse.Capture;
            }

            if (!_mouseOver) return GH_ObjectResponse.Release;
            _mouseOver = false;
            sender.Invalidate();
            return GH_ObjectResponse.Release;
        }

        internal void BasicResponseIntegration()
        {
            _mouseDown = true;
            Owner.RecordUndoEvent("Recorded event", (IGH_UndoAction)new GH_GenericObjectAction((IGH_DocumentObject)Owner));
        }
    }
}
