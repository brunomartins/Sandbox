using Grasshopper.Kernel;
using SandboxGh.Attributes;
using System;

namespace SandboxGh.UtilityTools
{
    public class EditValueDictionary : SandboxComponent
    {
        public EditValueDictionary()
          : base("This component allows to change the value of a select key of a dictionary", "Utility")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
        }

        protected override System.Drawing.Bitmap Icon => Resources.EditValueDictIcon;

        public override Guid ComponentGuid => new Guid("2DD78996-31C9-44A8-A884-BB4AAC9741A0");
    }
}