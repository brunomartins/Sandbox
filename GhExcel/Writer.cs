using Core;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GhExcel
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
            pManager.AddTextParameter("Sheet", "S", "The name of the sheet where the data will be written.", GH_ParamAccess.item);
            pManager.AddTextParameter("Headers", "H", "The headers for each columns.", GH_ParamAccess.list);
            pManager.AddGenericParameter("Data", "D", "The data to write into the spreadsheet.", GH_ParamAccess.tree);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = String.Empty;
            string sheet = String.Empty;
            string fileName = String.Empty;
            List<string> headers = new List<string>();
            bool canWrite = false;

            if (!DA.GetDataTree(5, out GH_Structure<IGH_Goo> data)) return;
            if (!DA.GetDataList<string>(4, headers)) return;

            if (data.Branches.Any(branch => branch.Count != data.get_Branch(0).Count))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Inconsistency number of elements between the branches.");
                return;
            }

            if (headers.Count != data.Branches.Count)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Inconsistency between number of headers and branches.");
                return;
            }

            DA.GetData(0, ref canWrite);
            if (!canWrite) return;

            if (!DA.GetData(1, ref path) || !DA.GetData(2, ref fileName) || !DA.GetData(3, ref sheet)) return;

            int rows = data.Branches.Count;
            int columns = data.Branches[0].Count;
            object[][] dataObject = new object[rows][];

            for (int i = 0; i < rows; i++)
            {
                dataObject[i] = new object[columns];
                for (int j = 0; j < columns; j++)
                {
                    dataObject[i][j] = data[i][j].ToString();
                }
            }

            this.Message = SpreadSheetWriter.Excel(path, fileName, sheet, headers.ToArray(), dataObject, canWrite);
        }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override Bitmap Icon => Resources.ExcelWriterIcon;

        public override Guid ComponentGuid => new Guid("EC05146F-6274-4E07-B092-DEE40FED7DF9");
    }
}