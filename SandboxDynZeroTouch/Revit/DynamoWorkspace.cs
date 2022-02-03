using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dynamo;
using Dynamo.Graph.Workspaces;

namespace Sandbox.Revit
{
    public static class DynamoWorkspace
    {

        private static HomeWorkspaceModel _model;
        private static bool activeEvaluate = false;

        internal static void Register(HomeWorkspaceModel dynamoModel)
        {
            // Subscribe our event handler methods to Dynamo
            dynamoModel.EvaluationCompleted += OnEvaulationCompleted;
            _model = dynamoModel;
        }
        internal static void Unregister()
        {
            // Unsubscribe our event handler methods
            _model.EvaluationCompleted -= OnEvaulationCompleted;
        }

        /// <summary>
        /// When the graph is evaluated .. ?
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="evaluationCompletedEventArgs"></param>
        private static void OnEvaulationCompleted(object sender, Dynamo.Models.EvaluationCompletedEventArgs evaluationCompletedEventArgs)
        {

            var model = Dynamo.Applications.DynamoRevit.RevitDynamoModel.CurrentWorkspace as HomeWorkspaceModel;
            int NodeCount = 0;
            foreach (Dynamo.Graph.Nodes.NodeModel node in model.Nodes)
            {

                if (node.Description.StartsWith("MMRunEvent"))
                {
                    node.MarkNodeAsModified(true);
                    if (node.CreationName.ToString() == "Sandbox.Revit.DynamoWorkspace.ForcePreviousNodeUpdate@var[]")
                    {
                        //node.InputNodes.Values.First().Item2.MarkNodeAsModified(true);
                        node.InputNodes.First().Value.Item2.MarkNodeAsModified(true);
                    }
                    NodeCount++;
                }
            }
            
            if (NodeCount == 0)
            {
                Unregister();
                activeEvaluate = false;
            }
            
        }

        /// <summary>
        /// MMRunEvent - Graph must be in manual mode to pass this node, otherwise an empty list is returned.
        /// </summary>
        /// <param name="List">Any List.</param>
        /// <param name="Out">Returns empty list if graph in manual.</param>
        /// <returns>A sheet read to be written.</returns>
        /// <search>flow, control</search>
        public static object FlowAllowedIfManual(List<object> list)
        {
            var model = Dynamo.Applications.DynamoRevit.RevitDynamoModel.CurrentWorkspace as HomeWorkspaceModel;
            if (activeEvaluate == false)
            {
                Register(model);
                activeEvaluate = true;
            }
            List<string> l = new List<string>();
            if (model.RunSettings.RunType.ToString() != "Manual")
            { list.Clear(); }
            return list;
        }
        /// <summary>
        /// MMRunEvent - Graph must be in manual mode to pass this node, otherwise an empty list is returned. Must be used with "ForceIndiviadualNodeRun" renamed with a prfix of "*Force".
        /// </summary>
        /// <param name="List">Any List.</param>
        /// <param name="Out">Returns empty list if graph in manual.</param>
        /// <returns>A sheet read to be written.</returns>
        /// <search>flow, control</search>
        public static object ForcePreviousNodeUpdate(List<object> list)
        {
            var model = Dynamo.Applications.DynamoRevit.RevitDynamoModel.CurrentWorkspace as HomeWorkspaceModel;
            foreach (Dynamo.Graph.Nodes.NodeModel node in model.Nodes)
            {
                if (activeEvaluate == false)
                {
                    Register(model);
                    activeEvaluate = true;
                }
            }
            return "Active = "+activeEvaluate.ToString();
        }

    }
}
