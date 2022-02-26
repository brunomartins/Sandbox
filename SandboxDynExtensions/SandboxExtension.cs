using Autodesk.DesignScript.Runtime;
using Dynamo.Graph.Nodes;
using Dynamo.Graph.Workspaces;
using Dynamo.Wpf.Extensions;
using SandboxDynExtensions.Menu;
using SandboxPackage = SandboxCore.Utilities;

namespace SandboxDynExtensions
{
    [IsVisibleInDynamoLibrary(false)]
    public class SandboxExtension : IViewExtension
    {
        private SandboxMenu _sandboxMenu;

        public void Dispose()
        {
        }

        public void Startup(ViewStartupParams viewStartupParams)
        {
            _sandboxMenu = new SandboxMenu(SandboxPackage.Package.DynamoDir());
        }

        public void Loaded(ViewLoadedParams viewLoadedParams)
        {
            _sandboxMenu.AddMenuToDynamo(viewLoadedParams);
            viewLoadedParams.CurrentWorkspaceChanged += ViewLoadedParamsOnCurrentWorkspaceChanged;
            viewLoadedParams.CurrentWorkspaceModel.NodeAdded += ObjOnNodeAdded;
        }

        private void ViewLoadedParamsOnCurrentWorkspaceChanged(IWorkspaceModel obj)
        {
            obj.NodeAdded -= ObjOnNodeAdded;
            obj.NodeAdded += ObjOnNodeAdded;
        }

        private void ObjOnNodeAdded(NodeModel obj)
        {

            if (string.IsNullOrEmpty(obj.Name) && obj.Category.Contains("Sandbox"))
            {
                obj.Name = $"Sandbox | {obj.Name}";
            }
        }

        public void Shutdown()
        {
        }

        public string UniqueId => "3B234622-43B7-4EA8-86DA-54FB390BE29E";
        public string Name => "NodeExtension";
    }
}