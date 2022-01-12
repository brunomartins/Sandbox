using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using SandboxCore;
using SandboxGh.Attributes;

namespace SandboxGh.ExcelTools
{
    public class ExcelWriter : SandboxComponent
    {
        public ExcelWriter()
          : base("This component write data into an excel form (xlsx).", "ExcelTools")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Write", "W", "If True the file is written.", GH_ParamAccess.item);
            pManager.AddTextParameter("Path", "P", "The directory where the excel will be created.", GH_ParamAccess.item);
            pManager.AddTextParameter("FileName", "FN", "The file name.", GH_ParamAccess.item);
            pManager.AddGenericParameter("DataSheet", "S", "The sheets will be written into the workbook.", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = String.Empty;
            string fileName = String.Empty;
            List<SandboxCore.SpreadSheet.DataSheet> sheets = new List<SandboxCore.SpreadSheet.DataSheet>();
            bool canWrite = false;

            DA.GetData(0, ref canWrite);
            if (!canWrite) return;

            if (!DA.GetDataList<SandboxCore.SpreadSheet.DataSheet>(3, sheets)) return;
            if (!DA.GetData(1, ref path) || !DA.GetData(2, ref fileName)) return;

            if (sheets.Count == 0 || sheets == null)
            {
                throw new Exception("Sheets can't be empty.");
            }

            this.Message = SpreadSheetWriter.Excel(path, fileName, sheets.ToArray(), canWrite);
        }

        protected override Bitmap Icon => Resources.ExcelWriterIcon;

        public override Guid ComponentGuid => new Guid("EC05146F-6274-4E07-B092-DEE40FED7DF9");
    }
}