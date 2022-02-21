using System;
using System.Collections.Generic;
using System.IO;
using Grasshopper.Kernel;
using SandboxCore.Utility;
using SandboxGh.Attributes;

namespace SandboxGh.Utility
{
    public class CSVFromDictionary : SandboxComponent
    {
        public CSVFromDictionary()
            : base("Turns a dictionary into a csv file.", "Utilities")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            //ToDo: Add bool for printing.
            pManager.AddParameter(new DictParam(), "Dict", "D", "Dictionary to convert to csv", GH_ParamAccess.item);
            pManager.AddTextParameter("Path", "P", "The directory where the csv will be created.", GH_ParamAccess.item);
            pManager.AddTextParameter("FileName", "FN", "The file name to create.", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var ghDict = new GH_Dict();
            string path = string.Empty;
            string fileName = string.Empty;
            if (!DA.GetData(0, ref ghDict)|!DA.GetData(1, ref path)|!DA.GetData(2, ref fileName))return;

            string dictionaryPath = $"{path}\\{fileName}.csv";
            string csv = string.Empty;
            var dictToCSV = new Dictionary<string, object>();

            GH_Dict.ToDictionary(ghDict, ref dictToCSV);
            DictionaryHelper.ToCsv(dictToCSV, ref csv);

            try
            {
                //ToDo: Check if file exist, print a new version.
                File.WriteAllText(dictionaryPath, csv);
                Message = "CSV Created";
            }
            catch (Exception e)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, e.Message);
            }
        }

        protected override System.Drawing.Bitmap Icon => Resources.CSVFromDictIcon;

        public override Guid ComponentGuid => new Guid("0CFF43E9-4940-4885-9566-42CAB413DFA5");
    }
}
