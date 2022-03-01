using System.Windows.Forms;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using SandboxGh.Attributes;

namespace SandboxGh.Data
{
    internal class CSVFromDictionaryButton : ButtonAttribute
    {
        public CSVFromDictionaryButton(IGH_Component component, string buttonText) : base(component, buttonText)
        {
        }

        public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (e.Button != MouseButtons.Left || (double)sender.Viewport.Zoom < 0.5 ||
                !_buttonArea.Contains(e.CanvasLocation))
            {
                return base.RespondToMouseDown(sender, e);
            }

            if (!(Owner is CSVFromDictionary component)) return GH_ObjectResponse.Handled;
            BasicResponseIntegration();
            component.WriteCSV(sender, e);
            component.ExpireSolution(true);

            return GH_ObjectResponse.Capture;
        }
    }
}
