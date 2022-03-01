using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SandboxCore.Utilities;
using SandboxCore.Utilities.Github;

namespace SandboxRevit.Support
{
    [Transaction(TransactionMode.ReadOnly)]
    class Updates : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var releaseHelper = new ReleaseHelper(Package.RevitDir());
            releaseHelper.CheckForUpdates(null, null);
            return Result.Succeeded;
        }
    }
}
