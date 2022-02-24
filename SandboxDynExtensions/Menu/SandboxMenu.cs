using Autodesk.DesignScript.Runtime;
using Dynamo.ViewModels;
using Dynamo.Wpf.Extensions;
using SandboxCore.Utilities.Github;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using SandboxPackage = SandboxCore.Utilities;

namespace SandboxDynExtensions.Menu
{
    [IsVisibleInDynamoLibrary(false)]
    public class SandboxMenu
    {
        private readonly ReleaseHelper _releaseHelper;
        private ViewLoadedParams _viewLoadedParams;

        public SandboxMenu(string sandboxDir)
        {
            _releaseHelper = new ReleaseHelper(sandboxDir);
        }

        public void AddMenuToDynamo(ViewLoadedParams viewLoadedParams)
        {
            _viewLoadedParams = viewLoadedParams;

            MenuItem mottMacMenu = new MenuItem { Header = "Sandbox" };

            var checksForUpdates = new MenuItem { Header = "Checks for updates" };
            checksForUpdates.Click += new RoutedEventHandler(_releaseHelper.ChecksForUpdates);

            var downloadUpdates = new MenuItem { Header = "Download updates" };
            downloadUpdates.Click += new RoutedEventHandler(_releaseHelper.ChecksForUpdates);

            var documentation = new MenuItem { Header = "Sandbox Documentation" };
            documentation.Click += new RoutedEventHandler(_releaseHelper.SandboxDocumentation);

            var exampleFile = new MenuItem { Header = "Example file" };
            exampleFile.Click += new RoutedEventHandler(OpenExampleFile);

            mottMacMenu.Items.Add(checksForUpdates);
            mottMacMenu.Items.Add(downloadUpdates);
            mottMacMenu.Items.Add(documentation);
            mottMacMenu.Items.Add(exampleFile);

            viewLoadedParams.dynamoMenu.Items.Add(mottMacMenu);
        }

        private void OpenExampleFile(object sender, EventArgs e)
        {
            string path = SandboxPackage.Package.DynExampleFileDir();
            if (!File.Exists(path))
            {
                MessageBox.Show($"Strange! The file should be stored here \n {path}");
            }

            var dynViewModel = _viewLoadedParams.DynamoWindow.DataContext as DynamoViewModel;
            dynViewModel?.OpenCommand.Execute(path);
        }
    }
}
