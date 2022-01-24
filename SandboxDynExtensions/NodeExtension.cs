using Autodesk.DesignScript.Runtime;
using Dynamo.Graph.Nodes;
using Dynamo.Graph.Workspaces;
using Dynamo.Wpf.Extensions;

namespace SandboxDynExtensions
{
    [IsVisibleInDynamoLibrary(false)]
    public class NodeExtension : IViewExtension
    {
        public void Dispose()
        {
        }

        public void Startup(ViewStartupParams viewStartupParams)
        {
        }

        public void Loaded(ViewLoadedParams viewLoadedParams)
        {
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