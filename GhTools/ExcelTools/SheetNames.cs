using System;
using Core;
using GhTools.Attributes;
using Grasshopper.Kernel;

namespace GhTools.ExcelTools
{
    public class SheetNames : MMComponent
    {
        public SheetNames()
          : base("ExcelSheets", "ExcelSheets",
              "Reads the sheets into the file.", "ExcelTools")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("FilePath", "FP", "The directory of the file.", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("SheetsName", "SN", "The sheets name present into the file.", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = String.Empty;
            if (!DA.GetData(0, ref path)) return;

            DA.SetDataList(0, SpreadSheetReader.GetSheetNames(path));
        }

        protected override System.Drawing.Bitmap Icon => Resources.SheetIcon;

        public override Guid ComponentGuid => new Guid("26C73224-9188-4E5F-86D4-2032A25CB9A3");
    }
}