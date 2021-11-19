﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace DynTools.ExcelTool
{
    /// <summary>Excel workbook creation.</summary>
    public static class Writer
    {
        /// <summary>
        /// Saves an excel file at the given path.
        /// </summary>
        /// <param name="path">The directory where to save the file.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="sheetNames">The name of sheet, where the data will be stored.</param>
        /// <param name="headers">The headers, matching the number of columns.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="save">True the file is written.</param>
        /// <returns>An information message of the result.</returns>
        /// <search>excel, write</search>
        public static string Excel(string path, string fileName, string[] sheetNames, Dictionary<string, string[]> headers, Dictionary<string, object[][]> data, bool save)
        {
            if (data.Any(branch => branch.Value.Length != data.First().Value.Length))
            {
                throw new Exception("Inconsistency number of elements between the branches.");
            }

            if (sheetNames.Length != data.Count || sheetNames.Length != headers.Count)
            {
                throw new Exception("Inconsistency data structure between sheetNames, headers and data.");
            }

            return SpreadSheetWriter.Excel(path, fileName, sheetNames, headers, data, save);
        }
    }
}
