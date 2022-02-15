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
            //Check for \ in filepath
            if (!path[path.Length - 1].Equals(@"\"))
            {
                path += @"\";
            }
            //Check for ".csv" in filename
            //This part seems very convoluted but I don't know how to check the ending of the string without first checking if it is over 4 characters
            string filePath;
            if (fileName.Length > 4)
            {
                if (fileName.Substring(fileName.Length - 4).Equals(".csv"))
                {
                    filePath = path + fileName;
                }
                else
                {
                    filePath = path + fileName + ".csv";
                }
            }
            else
            {
                filePath = path + fileName + ".csv";
            }
            /*
            string csv = String.Empty;  
            foreach (var kvp in ghDict.Value)
            {
                csv = csv + kvp.Key + ", " + kvp.Value + "\n";
            }
            foreach (string key in ghDict.Value.Keys)
            {

            }
            */

            var csvDict = ghDict.Value.ToDictionary(x => x.Key, x => x.Value);
            string csv = NestedDictIteration(csvDict).ToString();
            File.WriteAllText(filePath, csv);
            DA.SetData(0, csv);
        }

        private static object NestedDictIteration(Dictionary<string, IGH_Goo> nestedDict)
        {
            string csvEntry = String.Empty;
            foreach (var kvp in nestedDict)
            {
                csvEntry += kvp.Key;
                if (kvp.Value is Dictionary<string, IGH_Goo>)
                {
                    var nextLevel = nestedDict[kvp.Key];
                    NestedDictIteration((Dictionary<string, IGH_Goo>)nextLevel);
                }
                else
                {
                    csvEntry += (", " + kvp.Value.GetType() + "\n");
                }
            }
            return csvEntry;
        }

        protected override System.Drawing.Bitmap Icon => Resources.CSVFromDictIcon;

        public override Guid ComponentGuid => new Guid("0CFF43E9-4940-4885-9566-42CAB413DFA5");
    }
}
