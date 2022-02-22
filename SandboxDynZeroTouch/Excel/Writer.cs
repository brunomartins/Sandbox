using System;
using System.Collections.Generic;
using SandboxCore;
using SandboxCore.SpreadSheet;
using SandboxCore.Utility;
using System.IO;

namespace Sandbox.Excel
{
    /// <summary>Excel workbook creation.</summary>
    public static class Writer
    {
        /// <summary>
        /// Saves an excel file at the given path.
        /// </summary>
        /// <param name="path">The directory where to save the file.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="sheets">The sheets that will be printed into the workbook.</param>
        /// <param name="save">True the file is written.</param>
        /// <returns>An information message of the result.</returns>
        /// <search>excel, write</search>
        public static string Excel(string path, string fileName, DataSheet[] sheets, bool save)
        {
            if (sheets.Length == 0 || sheets == null)
            {
                throw new Exception("Sheets can't be empty.");
            }

            return SpreadSheetWriter.Excel(path, fileName, sheets, save);
        }

        /// <summary>
        /// Creates a sheet with the data required.
        /// </summary>
        /// <param name="sheetName">The name of sheet, where the data will be stored.</param>
        /// <param name="headers">The headers, matching the number of columns.</param>
        /// <param name="data">The data to write.</param>
        /// <returns>A sheet read to be written.</returns>
        /// <search>excel, write</search>
        public static DataSheet DataSheet(string sheetName, string[] headers, object[][] data)
        {
            return new DataSheet(sheetName, headers, data);
        }

        /// <summary>
        /// Creates a CSV file from a dictionary.
        /// </summary>
        /// <param name="dict">Dictionary to convert to CSV.</param>
        /// <param name="path">The directory where the CSV will be created.</param>
        /// <param name="fileName">The file name to create.</param>
        /// <param name="create">If true the csv will be created.</param>
        /// <returns>If the file is created will return the path.</returns>
        /// <search>csv, write</search>
        public static string CsvFromDictionary(Dictionary<string, object> dict, string path, string fileName, bool create)
        {
            string dictionaryPath = $"{path}\\{fileName}.csv";
            string csv = string.Empty;

            DictionaryHelper.ToCsv(dict, ref csv);
            if (!create) return string.Empty;

            try
            {
                File.WriteAllText(dictionaryPath, csv);
                return dictionaryPath;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}