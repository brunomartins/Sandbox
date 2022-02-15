using Grasshopper.Kernel;
using SandboxGh.Attributes;
using System;
using System.Linq;

namespace SandboxGh.Utility
{
    public class ValueTypeDictionary : SandboxComponent
    {
        public ValueTypeDictionary()
          : base("This component returns the type of the values in a dictionary, eg. 'text' or 'curve'", "Utilities")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Dict", "D", "The dictionary to extract the value types from.", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Types", "T", "The types of the values.", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var ghDict = new GH_Dict();
            if (!DA.GetData(0, ref ghDict)) return;

            var types = ghDict.Value.Values.Select(value => value.GetType());
            DA.SetDataList(0, types);
        }

        protected override System.Drawing.Bitmap Icon => Resources.ValueTypeDictIcon;

        public override Guid ComponentGuid => new Guid("2DD78996-31C9-44A8-A884-BB4AAC9741A2");
    }
}