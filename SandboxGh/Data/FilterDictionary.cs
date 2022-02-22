using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SandboxGh.Attributes;

namespace SandboxGh.Data
{
    public class FilterDictionary : SandboxComponent
    {
        public FilterDictionary()
          : base("Match the keys of the dictionary against a pattern.", "Utilities")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Dict", "D", "Dictionary", GH_ParamAccess.item);
            pManager.AddTextParameter("Pattern", "P", "Wildcard pattern for matching.", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Filtered dictionary", "F", "The dictionary result after the filter.", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var ghDict = new GH_Dict();
            string pattern = string.Empty;
            if (!DA.GetData(0, ref ghDict) && ghDict.Value == null) return;
            DA.GetData(1, ref pattern);
            if (string.IsNullOrEmpty(pattern)) DA.SetData(0, ghDict.Value); ;

            Dictionary<string, object> filteredDict = ghDict.Value.Where(kvp =>
                    LikeOperator.LikeString(kvp.Key, pattern, CompareMethod.Binary))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            DA.SetData(0, filteredDict);
        }

        protected override System.Drawing.Bitmap Icon => Resources.FilterDictIcon;

        public override Guid ComponentGuid => new Guid("FD0E8696-15EA-4E06-9182-D4569C830AB1");
    }
}