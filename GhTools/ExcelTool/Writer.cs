using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Core;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

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
            pManager.AddTextParameter("Sheet", "S", "The name of the sheet where the data will be written.", GH_ParamAccess.list);
            pManager.AddTextParameter("Headers", "H", "The headers for each columns.", GH_ParamAccess.tree);
            pManager.AddGenericParameter("Data", "D", "The data to write into the spreadsheet.", GH_ParamAccess.tree);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = String.Empty;
            List<string> sheets = new List<string>();
            string fileName = String.Empty;
            bool canWrite = false;

            if (!DA.GetDataTree(4, out GH_Structure<GH_String> headers)) return;
            if (!DA.GetDataTree(5, out GH_Structure<IGH_Goo> data)) return;
            if (!DA.GetDataList<string>(3, sheets)) return;


            if (data.Branches.Any(branch => branch.Count != data.get_Branch(0).Count))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Inconsistency number of elements between the branches.");
                return;
            }

            if (sheets.Count != headers.Branches.Count)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Inconsistency number of sheets and the header structure.");
                return;
            }

            DA.GetData(0, ref canWrite);
            if (!canWrite) return;

            if (!DA.GetData(1, ref path) || !DA.GetData(2, ref fileName)) return;

            Dictionary<string, string[]> headersDic = new Dictionary<string, string[]>();
            Dictionary<string, object[][]> dataObjectDic = new Dictionary<string, object[][]>();

            for (int s = 0; s < sheets.Count; s++)
            {
                string message = null;
                string mask = "{" + s + ";*}";
                GH_TreeRules treeRule = GH_TreeRules.FromString(mask, ref message);
                if (message != null)
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, message);
                    return;
                }

                // Filtering the branches.
                DataTree<IGH_Goo> selectedBranches = new DataTree<IGH_Goo>();
                for (int i = 0; i < data.Branches.Count; i++)
                {
                    GH_Path dataPath = data.get_Path(i);
                    if (treeRule.Apply(dataPath, i))
                    {
                        GH_Path tempPath = new GH_Path(i);
                        selectedBranches.AddRange(data.Branches[i], tempPath);
                    }
                }

                // Converting the headers.
                var sheetHeaders = headers.Branches[s];
                var convertedHeaders = sheetHeaders.Select(val => val.Value).ToArray();

                if (convertedHeaders.Length != selectedBranches.Branches.Count)
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Inconsistency between number of headers and branches.");
                    return;
                }

                int rows = selectedBranches.BranchCount;
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

                headersDic.Add(sheets[s], convertedHeaders);
                dataObjectDic.Add(sheets[s], dataObject);
            }

            this.Message = SpreadSheetWriter.Excel(path, fileName, sheets.ToArray(), headersDic, dataObjectDic, canWrite);
        }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override Bitmap Icon => Resources.ExcelWriterIcon;

        public override Guid ComponentGuid => new Guid("EC05146F-6274-4E07-B092-DEE40FED7DF9");
    }
}