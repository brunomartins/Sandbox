using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using IronXL;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using Core;
using Grasshopper.Kernel.Types;

namespace GhExcel
{
    public class GhExcelReader : GH_Component
    {
        public GhExcelReader()
          : base("ExcelReader", "ExcelReader",
            "This component reads these formats: xlsx, xlsm, csv.",
            PackageInfo.Category, PackageInfo.SubCategory)
        {
        }

        public override void CreateAttributes()
        {
            base.m_attributes = new GhExcelCustomAttributes(this);
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("FilePath", "FP", "The directory of the file.", GH_ParamAccess.item);
            pManager.AddGenericParameter("Sheets", "SH", "List of sheets to read, you can pass the name of sheet as a string or integer as the order. This is not need for csv file.", GH_ParamAccess.list);
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
            DataTree<object> data = new DataTree<object>();

            if (!DA.GetData(0, ref path)) return;
            if (!DA.GetDataList<object>(1, sheets)) return;
            DA.GetDataList(2, rowRanges);
            DA.GetDataList(3, columnRanges);

            int[] rowRange = new[] { 0, 0 };
            int[] columnRange = new[] { 0, 0 };

            // https://stackoverflow.com/questions/897796/how-do-i-open-an-already-opened-file-with-a-net-streamreader/898017#898017
            using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                WorkBook workbook = WorkBook.Load(fileStream);
                int[] dataPath = new int[2];

                for (int i = 0; i < sheets.Count; i++)
                {
                    WorkSheet sheet;
                    if (sheets[i] is GH_Number val)
                    {
                        sheet = workbook.WorkSheets[val.QC_Int()];
                    }
                    else
                    {
                        sheet = workbook.GetWorkSheet(sheets[i].ToString());
                    }

                    dataPath[0] = i;
                    rowRange[1] = sheet.RowCount;
                    columnRange[1] = sheet.ColumnCount;
                    int r = (sheets.Count > rowRanges.Count) ? 0 : i;
                    int c = (sheets.Count > columnRanges.Count) ? 0 : i;

                    if (rowRanges.Count != 0)
                    {
                        rowRange[0] = (int)rowRanges[r].T0;
                        rowRange[1] = (int)rowRanges[r].T1;
                    }

                    if (columnRanges.Count != 0)
                    {
                        columnRange[0] = (int)columnRanges[c].T0;
                        columnRange[1] = (int)columnRanges[c].T1;
                    }

                    for (int j = rowRange[0]; j <= rowRange[1]; j++)
                    {
                        dataPath[1] = j;
                        List<object> tempValues = new List<object>();

                        for (int k = columnRange[0]; k <= columnRange[1]; k++)
                        {
                            Cell cell = sheet.GetCellAt(j, k);
                            var cellValue = (cell == null) ? string.Empty : cell.Value;
                            tempValues.Add(cellValue);
                        }

                        data.AddRange(tempValues, new GH_Path(dataPath));
                    }
                }
            }

            DA.SetDataTree(0, data);
        }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override System.Drawing.Bitmap Icon => Resources.ExcelReaderIcon;

        public override Guid ComponentGuid => new Guid("AAE2D68B-B658-4E4D-8A01-DF42417986A6");
    }
}