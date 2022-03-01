﻿using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Reflection;

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

            PushButtonData version = new PushButtonData(
                "Version",
                "Version",
                _assemblyPath,
                "SandboxRevit.Support.Version");

            PushButtonData documentation = new PushButtonData(
                "Documentation",
                "Docs",
                _assemblyPath,
                "SandboxRevit.Support.Docs");

            PushButtonData help = new PushButtonData(
                "About",
                "About",
                _assemblyPath,
                "SandboxRevit.Support.About");

            // Stacked items for stacked buttons
            IList<RibbonItem> stackedSupport = panelSupport.AddStackedItems(help, documentation, version);

            // Defining buttons
            PushButton pbHelp = stackedSupport[0] as PushButton;
            pbHelp.ToolTip = "What is Sandbox";
            pbHelp.LongDescription = "Have a look what is Sandbox";
            //pbHelp.Image = new BitmapImage(new Uri("pack://application:,,,/BIMiconToolbar;component/Support/Help/Images/iconHelpSmall.png"));

            PushButton pbDocumentation = stackedSupport[1] as PushButton;
            pbDocumentation.ToolTip = "Documentation";
            pbDocumentation.LongDescription = "Check our online documentation";
            //pbDocumentation.Image = ;

            PushButton pbVersion = stackedSupport[2] as PushButton;
            pbVersion.ToolTip = "Display current version";
            pbVersion.LongDescription = "Retrieves current version";
            //pbVersion.Image = ;
            #endregion
        }
    }
}
