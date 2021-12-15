using System.Collections.Generic;
using SandboxCore;

namespace Sandbox.ExcelTools
{
    /// <summary>Excel workbook entry point.</summary>
    public static class Reader
    {
        /// <summary>
        /// Gets all the sheet names stored in the excel file.
        /// </summary>
        /// <param name="filePath">File filePath, use File.Path.</param>
        /// <returns>The sheet names.</returns>
        /// <search>excel, read, sheet</search>
        public static IEnumerable<string> SheetNames(string filePath)
        {
            return SpreadSheetReader.GetSheetNames(filePath);
        }

        /// <summary>
        /// Read the data of the workbook.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="sheet">List of sheet to read, you can pass the name of sheet as a string or integer as the order</param>
        /// <param name="cellRange">Ranges defining the number of rows and columns will be read. A standard area ref (e.g. "B1:D8").</param>
        /// <returns name="data">The excel content.</returns>
        /// <search>excel, read</search>
        public static IEnumerable<object[]> Excel(string filePath, object sheet, string cellRange = "")
        {
            return SpreadSheetReader.Excel(filePath, sheet, cellRange);
        }

        /// <summary>
        /// Read the data of the csv.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="cellRange">Ranges defining the number of rows and columns will be read. A standard area ref (e.g. "B1:D8").</param>
        /// <returns name="data">The csv content.</returns>
        /// <search>csv, read</search>
        public static IEnumerable<object[]> CSV(string filePath, string cellRange = "")
        {
            return SpreadSheetReader.Csv(filePath, cellRange);
        }
    }
}