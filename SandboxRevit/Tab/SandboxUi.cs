using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;

namespace SandboxRevit.Tab
{
    class SandboxUi
    {
        private static readonly string _availabilityName = "SandboxRevit.Tab.CommandAvailability";
        // Retrieve assembly path
        private static readonly string _assemblyPath = Assembly.GetExecutingAssembly().Location;

        public static void Toolbar(UIControlledApplication app)
        {
            // Create the Sandbox tab.
            string tabName = "Sandbox";
            app.CreateRibbonTab(tabName);

            // Add you ribbons here.
            // https://github.com/NiWeiNi/BIMiconToolbar/blob/aaa3d47a4ef5ffa7829ce74ab9656edaf2c125dd/Tab/BIMiconUI.cs
            RibbonPanel panelSupport = app.CreateRibbonPanel(tabName, "Support");

            #region Support panel

            PushButtonData version = new PushButtonData(
                "Version",
                "Version",
                _assemblyPath,
                "SandboxRevit.Support.Version.Version");

            PushButtonData documentation = new PushButtonData(
                "Documentation",
                "Docs",
                _assemblyPath,
                "SandboxRevit.Support.Docs.Docs");

            PushButtonData help = new PushButtonData(
                "About",
                "About",
                _assemblyPath,
                "SandboxRevit.Support.About.About");

            // Stacked items for stacked buttons
            IList<RibbonItem> stackedSupport = panelSupport.AddStackedItems(help, documentation, version);

            // Defining buttons
            PushButton pbHelp = stackedSupport[0] as PushButton;
            pbHelp.ToolTip = "What is Sandbox";
            pbHelp.LongDescription = "Have a look what is Sandbox";
            //pbHelp.Image = new BitmapImage(new Uri("pack://application:,,,/BIMiconToolbar;component/Support/Help/Images/iconHelpSmall.png"));
            pbHelp.AvailabilityClassName = _availabilityName;

            PushButton pbDocumentation = stackedSupport[1] as PushButton;
            pbDocumentation.ToolTip = "Documentation";
            pbDocumentation.LongDescription = "Check our online documentation";
            //pbDocumentation.Image = ;
            pbDocumentation.AvailabilityClassName = _availabilityName;

            PushButton pbVersion = stackedSupport[2] as PushButton;
            pbVersion.ToolTip = "Display current version";
            pbVersion.LongDescription = "Retrieves current version";
            //pbVersion.Image = ;
            pbVersion.AvailabilityClassName = _availabilityName;
            #endregion
        }
    }
}
