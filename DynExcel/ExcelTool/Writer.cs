using System;
using System.Linq;
using Core;

namespace DynExcel.ExcelTool
{
    /// <summary>Excel workbook creation.</summary>
    public static class Writer
    {
        /// <summary>
        /// Saves an excel file at the given path.
        /// </summary>
        /// <param name="path">The directory where to save the file.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="sheetName">The name of sheet, where the data will be stored.</param>
        /// <param name="headers">The headers, matching the number of columns.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="save">True the file is written.</param>
        /// <returns>An information message of the result.</returns>
        /// <search>excel, write</search>
        public static string Excel(string path, string fileName, string sheetName, string[] headers, object[][] data, bool save)
        {
            if (data.Any(branch => branch.Length != data[0].Length))
            {
                throw new Exception("Inconsistency number of elements between the branches.");
            }

            if (headers.Length != data.Length)
            {
                throw new Exception("Inconsistency between number of headers and branches.");
            }

            return SpreadSheetWriter.Excel(path, fileName, sheetName, headers, data, save);
        }
    }
}
