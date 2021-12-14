using System;
using System.Collections.Generic;
using System.Linq;
using GhTools.Attributes;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

namespace GhTools.ExcelTools
{
    public class DataSheet : MMComponent
    {
        public DataSheet()
            : base("DataSheet", "DataSheet",
                "Collect the data that will be written into a sheet.", "ExcelTools")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "N", "The name of the sheet where the data will be written.", GH_ParamAccess.item);
            pManager.AddTextParameter("Headers", "H", "The headers for each columns.", GH_ParamAccess.list);
            pManager.AddGenericParameter("Data", "D", "The data to write into the spreadsheet.", GH_ParamAccess.tree);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("DataSheet", "SH", "The sheet.", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string sheetName = String.Empty;
            List<string> headers = new List<string>();

            if (!DA.GetData(0, ref sheetName)) return;
            if (!DA.GetDataList<string>(1, headers)) return;
            if (!DA.GetDataTree(2, out GH_Structure<IGH_Goo> data)) return;

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

            DA.SetData(0, new Core.DataSheet(sheetName, headers.ToArray(), dataObject));
        }

        protected override System.Drawing.Bitmap Icon => Resources.DataSheetIcon;

        public override Guid ComponentGuid => new Guid("4154C2AC-5EE7-4C2F-A201-B84D7DA395B7");
    }
}
