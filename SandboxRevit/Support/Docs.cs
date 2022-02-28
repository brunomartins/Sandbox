using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Diagnostics;

namespace SandboxRevit.Support
{
    [Transaction(TransactionMode.ReadOnly)]
    class Docs : IExternalCommand
    {
        // ToDo: this need to use the method in the repo for the Sandbox menu
        private readonly string _docsWeb = "https://mirco-bianchini.gitbook.io/sandbox/";
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Process.Start(_docsWeb);
            return Result.Succeeded;
        }
    }
}
