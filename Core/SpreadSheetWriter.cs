using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Core
{
    public static class SpreadSheetWriter
    {
        public static string Excel(string path, string fileName, string sheetName, string[] headers, object[][] data, bool save)
        {
            string filePath = $"{path}\\{fileName}.xlsx";
            if (!save)
            {
                return $"The file will be saved here: {filePath}";
            }

            IWorkbook workbook = new XSSFWorkbook();
            ISheet excelSheet = workbook.CreateSheet(sheetName);

            // Set Font for header.
            IFont font = workbook.CreateFont();
            font.Color = HSSFColor.Grey80Percent.Index;
            font.IsBold = true;
            font.FontHeightInPoints = 12;

            // Set header style.
            ICellStyle headerCellStyle = workbook.CreateCellStyle();
            headerCellStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            headerCellStyle.FillPattern = FillPattern.SolidForeground;
            headerCellStyle.Alignment = HorizontalAlignment.Center;
            headerCellStyle.SetFont(font);

            int rawCount = data[0].Length + 1;

            for (int i = 0; i < rawCount; i++)
            {
                IRow row = excelSheet.CreateRow(i);
                if (i == 0)
                {
                    for (int j = 0; j < headers.Length; j++)
                    {
                        ICell cell = row.CreateCell(j, CellType.String);
                        cell.SetCellValue(headers[j]);
                        cell.CellStyle = headerCellStyle;
                    }
                    continue;
                }

                for (int j = 0; j < data.Length; j++)
                {
                    ICell cell = row.CreateCell(j, CellType.String);
                    cell.SetCellValue(data[j][i - 1].ToString());
                }
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (FileStream file = File.Create(filePath))
            {
                FileSecurity fileSecurity = File.GetAccessControl(filePath);
                SecurityIdentifier userAccount = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                fileSecurity.AddAccessRule(new FileSystemAccessRule(userAccount, FileSystemRights.FullControl,
                    AccessControlType.Allow));
                File.SetAccessControl(filePath, fileSecurity);
                workbook.Write(file);
                file.Close();
            }

            return "Writer succeed!";
        }
    }
}
