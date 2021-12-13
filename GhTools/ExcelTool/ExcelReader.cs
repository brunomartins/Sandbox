using Core;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using GhTools.Attributes;

namespace GhTools.ExcelTool
{
    public class ExcelReader : GH_Component
    {
        public ExcelReader()
          : base("ExcelReader", "ExcelReader",
            "This component reads these formats: xlsx, xlsm, xls.",
            PackageInfo.Category, "ExcelTools")
        {
        }

        public override void CreateAttributes()
        {
            base.m_attributes = new BaseComponentAttribute(this);
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("FilePath", "FP", "The directory of the file.", GH_ParamAccess.item);
            pManager.AddGenericParameter("Sheets", "SH", "Sheets to read, you can pass the name of sheet as a string or integer as the order. This is not need for csv file.", GH_ParamAccess.list);
            pManager.AddTextParameter("CellRange", "CR",
                "Intervals defining the number of rows and columns will be read. /n/" +
                "A standard area ref (e.g. B1:D8)", GH_ParamAccess.list, new List<string> { String.Empty });
            pManager[2].Optional = true;
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_StringParam("DataOutput", "DO", "The data from the excel", GH_ParamAccess.tree);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = String.Empty;
            List<object> sheets = new List<object>();
            List<string> cellRanges = new List<string>();

            if (!DA.GetData(0, ref path)) return;
            if (!DA.GetDataList<object>(1, sheets)) return;
            DA.GetDataList(2, cellRanges);

            DataTree<object> data = new DataTree<object>();

            for (int i = 0; i < sheets.Count; i++)
            {
                object sheetValue = (sheets[i] is GH_Number val) ? (object)val.QC_Int() : sheets[i].ToString();
                var cellRange = (cellRanges.Count < sheets.Count) ? cellRanges[0] : cellRanges[i];
                var result = SpreadSheetReader.Excel(path, sheetValue, cellRange);

                int count = 0;
                int[] dataPath = new int[2];
                dataPath[0] = i;
                foreach (object[] objects in result)
                {
                    dataPath[1] = count;
                    data.AddRange(objects, new GH_Path(dataPath));
                    count++;
                }
            }

            DA.SetDataTree(0, data);
        }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override System.Drawing.Bitmap Icon => Resources.ExcelReaderIcon;

        public override Guid ComponentGuid => new Guid("AAE2D68B-B658-4E4D-8A01-DF42417986A6");
    }
}