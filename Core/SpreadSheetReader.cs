using CsvHelper;
using CsvHelper.Configuration;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Core
{
    public static class SpreadSheetReader
    {
        public static IEnumerable<object[]> Csv(string filePath, string range)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("This file doesn't exist");
            }

            List<object[]> data = new List<object[]>();

            // https://stackoverflow.com/questions/897796/how-do-i-open-an-already-opened-file-with-a-net-streamreader/898017#898017
            using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var csvOptions = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    DetectColumnCountChanges = false,
                };

                var (firstRow, lastRow, firstColumn, lastColumn) = string.IsNullOrEmpty(range) ? (0, 0, 0, 0) : DefineSheetRange(range);

                var csv = new CsvParser(new StreamReader(fileStream, true), csvOptions);
                int rowCount = 0;
                while (csv.Read())
                {
                    if (rowCount < firstRow) continue;

                    var row = (firstColumn > 0 || lastColumn > 0)
                        ? csv.Record.Take(lastColumn).Skip(firstColumn)
                        : csv.Record;

                    data.Add(row.ToArray());
                    rowCount++;
                    if (rowCount > lastRow && lastRow != 0) break;
                }

                return data;
            }
        }

        public static IEnumerable<object[]> Excel(string filePath, object sheet, string range)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("This file doesn't exist");
            }

            List<object[]> data = new List<object[]>();

            using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IWorkbook package = WorkbookFactory.Create(fileStream);
                ISheet sheetObj = (sheet is string)
                    ? package.GetSheet(sheet.ToString())
                    : package.GetSheetAt(Convert.ToInt32(sheet));
                if (sheetObj == null)
                {
                    throw new ArgumentNullException($"The sheet {sheet} is not in the workbook.");
                }

                (int firstRow, int lastRow, int firstColumn, int lastColumn) sheetRange;
                if (string.IsNullOrEmpty(range))
                {
                    int lastColumn = 0;
                    for (int i = 0; i <= sheetObj.LastRowNum; i++)
                    {
                        lastColumn = Math.Max(lastColumn, sheetObj.GetRow(i).LastCellNum);
                    }
                    sheetRange = (sheetObj.FirstRowNum, sheetObj.LastRowNum, 0, lastColumn);
                }
                else
                {
                    sheetRange = DefineSheetRange(range);
                }

                for (int j = sheetRange.firstRow; j <= sheetRange.lastRow; j++)
                {
                    IRow row = sheetObj.GetRow(j);

                    if (row == null)
                    {
                        data.Add(Enumerable.Repeat("null", sheetRange.lastColumn - sheetRange.firstColumn).ToArray());
                        continue;
                    }

                    object[] tempValues = new object[sheetRange.lastColumn];
                    for (int k = sheetRange.firstColumn; k < sheetRange.lastColumn; k++)
                    {
                        tempValues[k] = row.GetCellValue(k);
                    }

                    data.Add(tempValues);
                }
            }

            return data;
        }

        public static IEnumerable<string> GetSheetNames(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("This file doesn't exist");
            }

            string fileExtension = Path.GetExtension(filePath);

            if (fileExtension == ".csv")
            {
                return new[] { Path.GetFileName(filePath).Split('.')[0] };
            }

            using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IWorkbook package = WorkbookFactory.Create(fileStream);

                string[] sheetNames = new string[package.NumberOfSheets];

                for (int i = 0; i < package.NumberOfSheets; i++)
                {
                    sheetNames[i] = package.GetSheetName(i);
                }

                return sheetNames;
            }
        }

        private static (int firstRow, int lastRow, int firstColumn, int lastColumn) DefineSheetRange(string sheetRange)
        {
            var rangeAddress = CellRangeAddress.ValueOf(sheetRange);
            return (rangeAddress.FirstRow, rangeAddress.LastRow, rangeAddress.FirstColumn, rangeAddress.LastColumn + 1);
        }
    }
}
