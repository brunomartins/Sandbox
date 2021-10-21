using Core;
using System.Collections.Generic;
using System.IO;

namespace DynExcel
{
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
        /// Gets the data into the selected sheet.
        /// </summary>
        /// <param name="filePath">File filePath, use File.Path.</param>
        /// <param name="sheet">List of sheet to read, you can pass the name of sheet as a string or integer as the order. This is not need for csv file.</param>
        /// <param name="cellRange">Ranges defining the number of rows and columns will be read. A standard area ref (e.g. "B1:D8").</param>
        /// <returns name="data">The excel sheet content.</returns>
        /// <search>excel read</search>
        public static IEnumerable<object[]> SheetData(string filePath, object sheet, string cellRange = "")
        {
            string fileExtension = Path.GetExtension(filePath);
            return (fileExtension == ".csv")
                ? SpreadSheetReader.Csv(filePath, cellRange)
                : SpreadSheetReader.Excel(filePath, sheet, cellRange);
        }
    }
}
