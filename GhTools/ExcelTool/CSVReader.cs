﻿using Core;
using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;

namespace GhTools.ExcelTool
{
    public class CSVReader : GH_Component
    {

        public CSVReader()
          : base("CSVReader", "CSVReader",
            "This component reads csv file.",
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
            pManager.AddTextParameter("CellRange", "CR",
                "Intervals defining the number of rows and columns will be read. /n/" +
                "A standard area ref (e.g. B1:D8)", GH_ParamAccess.list, new List<string> { String.Empty });
            pManager[1].Optional = true;
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_StringParam("DataOutput", "DO", "The data from the csv.", GH_ParamAccess.tree);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = String.Empty;
            string cellRanges = String.Empty;

            if (!DA.GetData(0, ref path)) return;
            if (!DA.GetData(1, ref path)) return;

            DataTree<object> data = new DataTree<object>();
            var result = SpreadSheetReader.Csv(path, cellRanges);

            foreach (object[] objects in result)
            {
                data.AddRange(objects);
            }

            DA.SetDataTree(0, data);
        }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override System.Drawing.Bitmap Icon => Resources.CSVReaderIcon;

        public override Guid ComponentGuid => new Guid("FAF86F13-EDAD-4295-A10F-5FD74E0E73B3");
    }
}
