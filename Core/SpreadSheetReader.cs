using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NPOI.SS.UserModel;

namespace Core
{
    public static class SpreadSheetReader
    {
        public static IEnumerable<object[]> Csv(string filePath, int[] rowRange, int[] columnRange)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("This file doesn't exist");
            }

            List<object[]> data = new List<object[]>();
            int[] setRowRange = new[] { 0, 0 };
            int[] setColumnRange = new[] { 0, 0 };

            // https://stackoverflow.com/questions/897796/how-do-i-open-an-already-opened-file-with-a-net-streamreader/898017#898017
            using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var csvOptions = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    DetectColumnCountChanges = false,
                };

                if (rowRange.Length != 0)
                {
                    AssignRowDimension(rowRange, ref setRowRange);
                }

                if (columnRange.Length != 0)
                {
                    AssignColumnsDimension(columnRange, ref setColumnRange);
                }

                var csv = new CsvParser(new StreamReader(fileStream, true), csvOptions);
                int rowCount = 0;
                while (csv.Read())
                {
                    if (rowCount < setRowRange[0]) continue;

                    var row = (setColumnRange[0] > 0 || setColumnRange[1] > 0)
                        ? csv.Record.Take(setColumnRange[1]).Skip(setColumnRange[0])
                        : csv.Record;

                    data.Add(row.ToArray());
                    rowCount++;
                    if (rowCount > setRowRange[1] && setRowRange[1] != 0) break;
                }

                return data;
            }
        }

        public static IEnumerable<object[]> Excel(string filePath, object sheet, int[] rowRange, int[] columnRange)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("This file doesn't exist");
            }

            List<object[]> data = new List<object[]>();
            int[] setRowRange = new[] { 0, 0 };
            int[] setColumnRange = new[] { 0, 0 };

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

                setRowRange[1] = sheetObj.LastRowNum;
                int d = 0;
                bool flag = true;
                while (flag)
                {
                    if (sheetObj.GetRow(d) != null) flag = false;
                    setColumnRange[1] = sheetObj.GetRow(d).LastCellNum;
                    d++;
                }
                if (rowRange.Length != 0)
                {
                    AssignRowDimension(rowRange, ref setRowRange);
                }

                if (columnRange.Length != 0)
                {
                    AssignColumnsDimension(columnRange, ref setColumnRange);
                }

                for (int j = setRowRange[0]; j < setRowRange[1]; j++)
                {
                    IRow row = sheetObj.GetRow(j);

                    if (row == null)
                    {
                        data.Add(Enumerable.Repeat("null", setColumnRange[1] - setColumnRange[0]).ToArray());
                        continue;
                    }

                    object[] tempValues = new object[setColumnRange[1]];
                    for (int k = setColumnRange[0]; k < setColumnRange[1]; k++)
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

        private static void AssignRowDimension(int[] rowInterval, ref int[] rowRange)
        {
            rowRange[0] = (int)rowInterval[0];
            rowRange[1] = (int)rowInterval[1];
        }

        private static void AssignColumnsDimension(int[] columnInterval, ref int[] columnRange)
        {
            columnRange[0] = (int)columnInterval[0];
            columnRange[1] = (int)columnInterval[1];
        }
    }
}
