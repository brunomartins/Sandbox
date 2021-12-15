using System;
using Grasshopper.Kernel;
using SandboxGh.Attributes;

namespace SandboxGh.UtilityTools
{
    public class ReadDict : MMComponent
    {
        public ReadDict()
            : base("Read Dictionary", "ReadDict",
                "Read key-value of a dictionary.", "Utilities")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Dict", "D", "Dictionary", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Key", "K", "List of keys.", GH_ParamAccess.list);
            pManager.AddGenericParameter("Value", "V", "List of values.", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var ghDict = new GH_Dict();
            if (!DA.GetData(0, ref ghDict)) return;
            DA.SetDataList(0, ghDict.Value.Keys);
            DA.SetDataList(1, ghDict.Value.Values);
        }

        protected override System.Drawing.Bitmap Icon => Resources.ReadDictIcon;

        public override Guid ComponentGuid => new Guid("0CFF43E9-4940-4885-9566-42CAB413DFAE");
    }
}
