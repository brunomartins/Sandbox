using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using Core;

namespace GhExcel
{
    public class GhExcelWriter : GH_Component
    {
        public GhExcelWriter()
          : base("ExcelWriter", "ExcelWriter",
              "This component write data into an excel form (xlsx). Data must be organized in a data tree where every branch matches the number of header.",
              PackageInfo.Category, PackageInfo.SubCategory)
        {
        }

        public override void CreateAttributes()
        {
            base.m_attributes = new GhExcelCustomAttributes(this);
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
            string sheetName = String.Empty;
            string fileName = String.Empty;
            List<string> headers = new List<string>();
            bool canWrite = false;

            if (!DA.GetDataTree(5, out GH_Structure<IGH_Goo> data)) return;
            if (!DA.GetDataList<string>(4, headers)) return;

            if (data.Branches.Any(branch => branch.Count != data.get_Branch(0).Count))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error,
                    "Inconsistency number of elements between the branches.");
                return;
            }

            if (headers.Count != data.Branches.Count)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error,
                    "Inconsistency between number of headers and branches.");
                return;
            }

            DA.GetData(0, ref canWrite);
            if (!canWrite) return;

            if (!DA.GetData(1, ref path) || !DA.GetData(2, ref fileName) || !DA.GetData(3, ref sheetName)) return;

            string filePath = $"{path}\\{fileName}.xlsx";
            WorkBook workBook = WorkBook.Create(ExcelFileFormat.XLSX);
            WorkSheet workSheet = workBook.CreateWorkSheet(sheetName);

            int rawCount = data.get_Branch(0).Count + 1;

            for (int i = 0; i < rawCount; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < headers.Count; j++)
                    {
                        workSheet.SetCellValue(i, j, headers[j]);
                        Cell cell = workSheet.GetCellAt(i, j);
                        cell.Style.BackgroundColor = "8B8B8B";
                        cell.Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;
                        cell.Style.Font.Bold = true;
                        cell.Style.Font.Color = "000000";
                    }

                    continue;
                }

                for (int j = 0; j < data.Branches.Count; j++)
                {
                    workSheet.SetCellValue(i, j, data.Branches[j][i - 1].ToString());
                }
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            workBook.SaveAs(filePath);
            FileSecurity fileSecurity = File.GetAccessControl(filePath);
            SecurityIdentifier userAccount = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            fileSecurity.AddAccessRule(new FileSystemAccessRule(userAccount, FileSystemRights.FullControl, AccessControlType.Allow));
            File.SetAccessControl(filePath, fileSecurity);
        }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override Bitmap Icon => Resources.ExcelWriterIcon;

        public override Guid ComponentGuid => new Guid("EC05146F-6274-4E07-B092-DEE40FED7DF9");
    }
}