using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SandboxCore.Utilities.Github;

namespace SandboxRevit.Support
{
    [Transaction(TransactionMode.ReadOnly)]
    class Docs : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            ReleaseHelper.SandboxDocumentation(null, null);
            return Result.Succeeded;
        }
    }
}
