using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using SandboxGh.Attributes;

namespace SandboxGh.Utility
{
    public class EditValueDictionary : SandboxComponent
    {
        public EditValueDictionary()
          : base("This component allows to change the value of a select key of a dictionary", "Utilities")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Dict", "D", "The dictionary to override the value.", GH_ParamAccess.item);
            pManager.AddTextParameter("Key", "K", "The key to search.", GH_ParamAccess.list);
            pManager.AddGenericParameter("Value", "V", "The value to change.", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Result dictionary", "R", "The dictionary with the overridden value.", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var ghDict = new GH_Dict();
            var keys = new List<string>();
            var values = new List<object>();
            if (!DA.GetData(0, ref ghDict) && ghDict.Value == null) return;
            if (!DA.GetDataList(1, keys) || !DA.GetDataList(2, values)) return;

            if (values.Count != keys.Count)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "You must supply an equal number of keys and values.");
                return;
            }

            Dictionary<string, object> dict = new Dictionary<string, object>(ghDict.Value);
            for (int i = 0; i < keys.Count; i++)
            {
                if (!dict.ContainsKey(keys[i]))
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Warning,
                        $"Key {keys[i]} at index {i} was skipped because it doesn't existed.");
                }
                else
                    dict[keys[i]] = values[i];
            }

            DA.SetData(0, new GH_Dict(dict));
        }

        protected override System.Drawing.Bitmap Icon => Resources.EditValueDictIcon;

        public override Guid ComponentGuid => new Guid("2DD78996-31C9-44A8-A884-BB4AAC9741A0");
    }
}