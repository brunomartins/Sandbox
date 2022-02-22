using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using SandboxGh.Attributes;
using System.Windows.Forms;

namespace SandboxGh.Utility
{
    internal class CreateDictionaryButton : ButtonAttribute
    {
        public CreateDictionaryButton(IGH_Component component, string buttonText) : base(component, buttonText)
        {
        }

        public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (e.Button != MouseButtons.Left || (double)sender.Viewport.Zoom < 0.5 ||
                !_buttonArea.Contains(e.CanvasLocation))
            {
                return base.RespondToMouseDown(sender, e);
            }

            if (!(Owner is CreateDictionary component)) return GH_ObjectResponse.Handled;
            BasicResponseIntegration();
            component.CreateValueList(sender, e);
            component.ExpireSolution(true);

            return GH_ObjectResponse.Capture;
        }
    }
}
