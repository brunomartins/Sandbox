using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Core;

namespace GhTools.Utilities
{
    public class CreateDict : GH_Component
    {
        public CreateDict()
          : base("Create Dictionary", "Create Dictionary",
              "Create a new dictionary",
              PackageInfo.Category, "Utilities")
        {
        }

        public override void CreateAttributes()
        {
            this.m_attributes = new CustomAttributes(this);
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Key", "K", "List of keys.", GH_ParamAccess.list);
            pManager.AddGenericParameter("Value", "V", "List of values.", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new GH_DictParam(), "Dict", "D", "New dictionary", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var keys = new List<string>();
            var values = new List<IGH_Goo>();

            if (!DA.GetDataList(0, keys)) return;
            if (!DA.GetDataList(1, values)) return;

            if (values.Count != keys.Count)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "You must supply an equal number of keys and values.");
                return;
            }

            var dict = new Dictionary<string, IGH_Goo>();
            for (int i = 0; i < keys.Count; i++)
            {
                if (!dict.ContainsKey(keys[i]))
                    dict.Add(keys[i], values[i]);
                else
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, $"Key {keys[i]} at index {i} was skipped because it already existed.");
            }

            var ghDict = new GH_Dict(dict);
            DA.SetData(0, ghDict);
        }

        protected override System.Drawing.Bitmap Icon => Resources.CreateDictIcon;

        public override Guid ComponentGuid => new Guid("70F44B2A-AED6-493D-978D-2AE0D9E9A15C");

        public override GH_Exposure Exposure => GH_Exposure.primary;
    }
}