using System.Collections.Generic;
using System.Drawing;

namespace SandboxCore.Utility
{
    public static class DictionaryHelper
    {
        /// <summary>
        ///  This function recursively iterates over a Dictionary to convert it to a csv formatted string.
        /// </summary>
        /// <param name="nestedDict">The Dictionary to iterate over.</param>
        /// <param name="csvEntry">The csv string that is concatenated on to. Provide an empty string or an existing csv string to be extended.</param>
        /// <param name="baseVal">Value used to maintain parent key information during recursion. As per default is empty.</param>
        public static void ToCsv(Dictionary<string, object> nestedDict, ref string csvEntry, string baseVal = "")
        {
            foreach (var kvp in nestedDict)
            {
                if (kvp.Value is Dictionary<string, object> dataValue)
                {
                    string newBase = $"{baseVal}\"{kvp.Key.Replace("\"", "\"\"")}\",";
                    ToCsv(dataValue, ref csvEntry, newBase);
                }
                else
                {
                    csvEntry += $"{baseVal}\"{kvp.Key.Replace("\"", "\"\"")}\",\"{kvp.Value.ToString().Replace("\"", "'")}\"\n";
                }
            }
        }
    }
}
