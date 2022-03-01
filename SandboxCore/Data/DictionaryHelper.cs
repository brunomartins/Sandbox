using System.Collections.Generic;

namespace SandboxCore.Data
{
    public static class DictionaryHelper
    {
        /// <summary>
        ///  This function recursively iterates over a Dictionary to convert it to a csv formatted string.
        /// </summary>
        /// <param name="dict">The Dictionary to iterate over.</param>
        /// <param name="csvEntry">The csv string that is concatenated on to. Provide an empty string or an existing csv string to be extended.</param>
        /// <param name="baseVal">Value used to maintain parent key information during recursion. As per default is empty.</param>
        public static void ToCsv(Dictionary<string, object> dict, ref string csvEntry, string baseVal = "")
        {
            foreach (var kvp in dict)
            {
                string root = $"{baseVal}\"{kvp.Key.Replace("\"", "\"\"")}\",";

                if (kvp.Value is Dictionary<string, object> dataValue)
                {
                    ToCsv(dataValue, ref csvEntry, root);
                }
                else
                {
                    string value = kvp.Value.ToString().Replace("\"", "'");

                    if (kvp.Value is ICollection<object> collection)
                    {
                        value = string.Join(",", collection);
                    }

                    csvEntry += $"{root}\"{value}\"\n";
                }
            }
        }
    }
}
