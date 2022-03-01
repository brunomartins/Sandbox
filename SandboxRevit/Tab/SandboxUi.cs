using System;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace SandboxRevit.Tab
{
    /// <summary>
    /// Creates the tab with all the ribbons in Revit.
    /// </summary>
    class SandboxUi
    {
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
            PushButtonData updates = new PushButtonData(
                "Updates",
                "Updates",
                _assemblyPath,
                "SandboxRevit.Support.Updates");

            PushButtonData documentation = new PushButtonData(
                "Documentation",
                "Docs",
                _assemblyPath,
                "SandboxRevit.Support.Docs");


            // Stacked items for stacked buttons
            IList<RibbonItem> stackedSupport = panelSupport.AddStackedItems(documentation, updates);

            // Defining buttons
            PushButton pbDocumentation = stackedSupport[0] as PushButton;
            pbDocumentation.ToolTip = "Documentation";
            pbDocumentation.LongDescription = "Check our online documentation";
            pbDocumentation.Image = new BitmapImage(new Uri("pack://application:,,,/SandboxRevit;component/Support/Images/DoumentationIcon.png"));

            PushButton pbVersion = stackedSupport[1] as PushButton;
            pbVersion.ToolTip = "Display current version";
            pbVersion.LongDescription = "Retrieves current version";
            pbVersion.Image = new BitmapImage(new Uri("pack://application:,,,/SandboxRevit;component/Support/Images/UpdatesIcon.png"));
            #endregion
        }
    }
}
