using Core;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.IO;

namespace GhExcel
{
    public class Reader : GH_Component
    {
        public Reader()
          : base("ExcelReader", "ExcelReader",
            "This component reads these formats: xlsx, xlsm, csv.",
            PackageInfo.Category, PackageInfo.SubCategory)
        {
        }

        public override void CreateAttributes()
        {
            base.m_attributes = new CustomAttributes(this);
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("FilePath", "FP", "The directory of the file.", GH_ParamAccess.item);
            pManager.AddGenericParameter("Sheets", "SH", "Sheets to read, you can pass the name of sheet as a string or integer as the order. This is not need for csv file.", GH_ParamAccess.list);
            pManager.AddIntervalParameter("RowRangeSelection", "RS", "Intervals defining the number of rows will be read.", GH_ParamAccess.list, new List<Interval>());
            pManager.AddIntervalParameter("ColumnRangeSelection", "CS", "Intervals defining the number of columns will be read.", GH_ParamAccess.list, new List<Interval>());
            pManager[2].Optional = pManager[3].Optional = true;
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_StringParam("DataOutput", "DO", "The data from the excel", GH_ParamAccess.tree);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = String.Empty;
            List<object> sheets = new List<object>();
            List<Interval> rowRanges = new List<Interval>();
            List<Interval> columnRanges = new List<Interval>();

            if (!DA.GetData(0, ref path)) return;
            if (!DA.GetDataList<object>(1, sheets)) return;
            DA.GetDataList(2, rowRanges);
            DA.GetDataList(3, columnRanges);

            DataTree<object> data = new DataTree<object>();

            for (int i = 0; i < sheets.Count; i++)
            {
                int r = (i >= rowRanges.Count) ? rowRanges.Count - 1 : i;
                int c = (i >= columnRanges.Count) ? columnRanges.Count - 1 : i;

                int[] rr = (rowRanges.Count == 0) ? Array.Empty<int>() : new[] { (int)rowRanges[r].T0, (int)rowRanges[r].T1 };
                int[] cr = (columnRanges.Count == 0) ? Array.Empty<int>() : new[] { (int)columnRanges[c].T0, (int)columnRanges[c].T1 };

                string fileExtension = Path.GetExtension(path);
                object sheetValue = (sheets[i] is GH_Number val) ? (object)val.QC_Int() : sheets[i].ToString();

                var result = (fileExtension == ".csv")
                    ? SpreadSheetReader.Csv(path, rr, cr)
                    : SpreadSheetReader.Excel(path, sheetValue, rr, cr);

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