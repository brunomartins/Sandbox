using Dynamo.Controls;
using Dynamo.Wpf;

namespace SandboxDynExplicitNode.About
{
    public class AboutCustomView : INodeViewCustomization<AboutSandbox>
    {
        public void CustomizeView(AboutSandbox model, NodeView nodeView)
        {
            var checkBoxUi = new CheckBox();
            nodeView.inputGrid.Children.Add(checkBoxUi);
            checkBoxUi.DataContext = model;
        }

        public void Dispose()
        {
        }
    }
}
