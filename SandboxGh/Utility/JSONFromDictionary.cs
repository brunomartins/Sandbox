using System;
using System.Linq;
using Grasshopper.Kernel;
using Newtonsoft.Json;
using SandboxGh.Attributes;

namespace SandboxGh.Utility
{
    public class JSONFromDictionary : SandboxComponent
    {
        public JSONFromDictionary()
            : base("Turns a dictionary into a JSON format.", "Utilities")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Dict", "D", "Dictionary", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("JSON", "J", "Resulting JSON.", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var ghDict = new GH_Dict();
            if (!DA.GetData(0, ref ghDict)) return;
            var jsonDict = ghDict.Value.ToDictionary(x => x.Key, x => x.Value.ToString());
            //NOTE ---- THIS WORKS FOR A SINGLE LAYER OF DICTIONARY. IF THERE IS ANY NESTING IT DOES NOT.
            DA.SetData(0, JsonConvert.SerializeObject(jsonDict, Formatting.Indented));
        }

        protected override System.Drawing.Bitmap Icon => Resources.JSONFromDictIcon;

        public override Guid ComponentGuid => new Guid("0CFF43E9-4940-4885-9566-42CAB413DFA9");
    }
}
