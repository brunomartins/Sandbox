using Core;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GhTools.ExcelTool
{
    public class Writer : GH_Component
    {
        public Writer()
          : base("ExcelWriter", "ExcelWriter",
              "This component write data into an excel form (xlsx). Data must be organized in a data tree where every branch matches the number of header.",
              PackageInfo.Category, PackageInfo.SubCategory)
        {
        }

        public override void CreateAttributes()
        {
            base.m_attributes = new CustomAttributes(this);
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Write", "W", "If True the file is written.", GH_ParamAccess.item);
            pManager.AddTextParameter("Path", "P", "The directory where the excel will be created.", GH_ParamAccess.item);
            pManager.AddTextParameter("FileName", "FN", "The file name.", GH_ParamAccess.item);
            pManager.AddGenericParameter("Sheet", "S", "The sheets will be written into the workbook.", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = String.Empty;
            string fileName = String.Empty;
            List<Core.Sheet> sheets = new List<Core.Sheet>();
            bool canWrite = false;

            DA.GetData(0, ref canWrite);
            if (!canWrite) return;

            if (!DA.GetDataList<Core.Sheet>(3, sheets)) return;
            if (!DA.GetData(1, ref path) || !DA.GetData(2, ref fileName)) return;

            if (sheets.Count == 0 || sheets == null)
            {
                throw new Exception("Sheets can't be empty.");
            }

            this.Message = SpreadSheetWriter.Excel(path, fileName, sheets.ToArray(), canWrite);
        }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override Bitmap Icon => Resources.ExcelWriterIcon;

        public override Guid ComponentGuid => new Guid("EC05146F-6274-4E07-B092-DEE40FED7DF9");
    }
}