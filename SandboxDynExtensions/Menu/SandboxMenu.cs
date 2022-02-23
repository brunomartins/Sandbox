using Autodesk.DesignScript.Runtime;
using Dynamo.Wpf.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SandboxDynExtensions.Menu
{
    [IsVisibleInDynamoLibrary(false)]
    public class SandboxMenu
    {
        public void AddMenuToDynamo(ViewLoadedParams viewLoadedParams)
        {
            MenuItem mottMacMenu = new MenuItem { Header = "Sandbox" };


            var checksForUpdates = new MenuItem { Header = "Checks for updates" };
            checksForUpdates.Click += (sender, args) =>
            {
                MessageBox.Show("Hello " + Environment.UserName);
            };

            var test = new MenuItem { Header = "Test", ToolTip = "Sandbox standard menu" };
            test.Click += new RoutedEventHandler(ChecksForUpdates);

            mottMacMenu.Items.Add(checksForUpdates);
            mottMacMenu.Items.Add(new Separator());
            mottMacMenu.Items.Add(test);

            viewLoadedParams.dynamoMenu.Items.Add(mottMacMenu);

        }

        internal void ChecksForUpdates(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("HelloBello!");
        }
    }
}
