using Grasshopper.Kernel;
using SandboxCore.Utility;
using SandboxGh.Attributes;
using System;
using System.Collections.Generic;
using System.IO;

namespace SandboxGh.Utility
{
    public class CSVFromDictionary : SandboxComponent
    {
        private string _filePath;
        private string _csv;

        public CSVFromDictionary()
            : base("Turns a dictionary into a csv file.", "Utilities")
        {
            m_attributes = new CSVFromDictionaryButton(this, "Create");
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
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
            if (!DA.GetData(0, ref ghDict) | !DA.GetData(1, ref path) | !DA.GetData(2, ref fileName)) return;

            _filePath = $"{path}\\{fileName}.csv";
            _csv = string.Empty;
            var dictToCSV = new Dictionary<string, object>();

            GH_Dict.ToDictionary(ghDict, ref dictToCSV);
            DictionaryHelper.ToCsv(dictToCSV, ref _csv);
        }


        /// <summary>
        /// Creates a csv file at the specified directory.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        internal void WriteCSV(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(_filePath, _csv);
                Message = "CSV Created";
            }
            catch (Exception ex)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, ex.Message);
            }
        }

        protected override System.Drawing.Bitmap Icon => Resources.CSVFromDictIcon;

        public override Guid ComponentGuid => new Guid("0CFF43E9-4940-4885-9566-42CAB413DFA5");
    }
}
