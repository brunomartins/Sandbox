using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using SandboxGh.Attributes;

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
            List<string> types = new List<string>();


            if (!DA.GetData(0, ref ghDict)) return;

            foreach (var item in ghDict.Value.Values)
            {
                types.Add(item.TypeName);   
            }

            DA.SetDataList(0, types);

        }

        protected override System.Drawing.Bitmap Icon => Resources.ValueTypeDictIcon;

        public override Guid ComponentGuid => new Guid("2DD78996-31C9-44A8-A884-BB4AAC9741A2");
    }
}