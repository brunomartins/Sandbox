using System;
using Dynamo.Graph.Nodes;
using ProtoCore.AST.AssociativeAST;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SandboxDynExplicitNode.About
{
    /// <summary>
    /// Wrapper for about class.
    /// This is mostly to show the icon in the Dynamo 2.0 library.
    /// This is also a template class useful to develop other nodes.
    /// </summary>
    [NodeDescription("Sandbox about")]
    [NodeCategory("Sandbox.AboutSandbox")]
    [InPortNames("N")]
    [InPortTypes("string")]
    [InPortDescriptions("Input your name")]
    [OutPortNames("R")]
    [OutPortTypes("string")]
    [OutPortDescriptions("Result")]
    [IsDesignScriptCompatible]
    public class AboutSandbox : NodeModel
    {
        private bool _aboutChecked;

        [JsonConstructor]
        private AboutSandbox(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {
        }

        public bool isChecked
        {
            get => _aboutChecked;
            set
            {
                _aboutChecked = value;
                RaisePropertyChanged("isChecked");
                OnNodeModified();
            }
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            if (!InPorts[0].Connectors.Any())
            {
                return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()) };
            }

            var checkValue = AstFactory.BuildBooleanNode(isChecked);
            var functionNode =
                AstFactory.BuildFunctionCall(
                    new Func<string, bool, string>(SandboxDynUtilities.Functions.About.AboutSandbox),
                    new List<AssociativeNode> { inputAstNodes[0], checkValue });

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionNode) };
        }
    }
}
