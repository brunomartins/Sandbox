using System.IO.Packaging;
using Autodesk.DesignScript.Runtime;
using Dynamo.Graph.Nodes;
using Dynamo.Graph.Workspaces;
using Dynamo.Wpf.Extensions;
using SandboxCore.Utilities.Github;
using SandboxPackage = SandboxCore.Utilities;
using SandboxDynExtensions.Menu;

namespace SandboxDynExtensions
{
    [IsVisibleInDynamoLibrary(false)]
    public class NodeExtension : IViewExtension
    {
        private SandboxMenu _sandboxMenu;
        private event Helper.DelEvent _gitEvents;
        private string _releaseVersion;
        private string _localVersion;

        public void Dispose()
        {
        }

        public void Startup(ViewStartupParams viewStartupParams)
        {
            _gitEvents += new Helper.DelEvent(Helper.GetLastTagRelease);
            _releaseVersion = _gitEvents.Invoke().Result;
            _gitEvents -= new Helper.DelEvent(Helper.GetLastTagRelease);

            _localVersion = SandboxPackage.Package.GetSandboxVersion(SandboxPackage.Package.DynamoDir());
            _sandboxMenu = new SandboxMenu();
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