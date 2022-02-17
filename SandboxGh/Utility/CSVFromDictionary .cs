using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using SandboxGh.Attributes;

namespace SandboxGh.Utility
{
    public class CSVFromDictionary : SandboxComponent
    {
        public CSVFromDictionary()
            : base("Turns a dictionary into a csv file as well as returning the csv string.", "Utilities")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Dict", "D", "Dictionary to convert to csv", GH_ParamAccess.item);
            pManager.AddTextParameter("Path", "P", "The directory where the csv will be created.", GH_ParamAccess.item);
            pManager.AddTextParameter("FileName", "FN", "The file name to create.", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("CSV", "C", "Resulting csv string.", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var ghDict = new GH_Dict();
            string path = String.Empty;
            string fileName = String.Empty;
            if (!DA.GetData(0, ref ghDict)) return;
            if (!DA.GetData(1, ref path)) return;
            if (!DA.GetData(2, ref fileName)) return;

            string filePath = path + @"\" + fileName + ".csv";

            string csv = String.Empty;
            string csvBase = String.Empty;


            csv = RecursiveDictionary(ghDict, csv, csvBase);
            File.WriteAllText(filePath, csv);
            DA.SetData(0, csv);
        }

        private string RecursiveDictionary(GH_Dict nestedDict, string csvEntry, string baseVal)
        {

            foreach (var kvp in nestedDict.Value)
            {
                if (kvp.Value is GH_Dict)
                {
                    string newBase = $"{baseVal}\"{kvp.Key.Replace("\"","\"\"")}\",";
                    var castVal = kvp.Value as GH_Dict;
                    csvEntry = RecursiveDictionary(castVal, csvEntry, newBase);
                }
                else
                {
                    csvEntry += $"{baseVal}\"{kvp.Key.Replace("\"", "\"\"")}\",\"{kvp.Value.ToString().Replace("\"", "'")}\"\n";
                }
            }
            return csvEntry;
        }

        protected override System.Drawing.Bitmap Icon => Resources.CSVFromDictIcon;

        public override Guid ComponentGuid => new Guid("0CFF43E9-4940-4885-9566-42CAB413DFA5");
    }
}
