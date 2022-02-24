using Grasshopper;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Plugin;
using SandboxCore.Utilities;
using SandboxCore.Utilities.Github;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SandboxGh.Attributes
{
    public class SandboxAssemblyPriority : GH_AssemblyPriority
    {
        private ToolStripMenuItem _checksForUpdates;
        private ToolStripMenuItem _downloadLastUpdate;
        private ToolStripMenuItem _documentations;
        private ToolStripMenuItem _exampleFile;
        private ReleaseHelper _releaseHelper;

        public override GH_LoadingInstruction PriorityLoad()
        {
            Instances.ComponentServer.AddAlias("col", new Guid("03AD926B-5642-402E-88B2-14F752595928"));
            Instances.CanvasCreated += new Instances.CanvasCreatedEventHandler(this.RegisterNewMenuItems);

            _releaseHelper = new ReleaseHelper(Package.SandboxGhAssemblyDir);
 
            return GH_LoadingInstruction.Proceed;
        }

        private void RegisterNewMenuItems(GH_Canvas canvas)
        {
            Instances.CanvasCreated -= new Instances.CanvasCreatedEventHandler(this.RegisterNewMenuItems);
            GH_DocumentEditor documentEditor = Instances.DocumentEditor;
            if (documentEditor == null)
                return;
            this.SetupSandboxMenu(documentEditor);
        }

        private void SetupSandboxMenu(GH_DocumentEditor docEditor)
        {
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
            docEditor.MainMenuStrip.SuspendLayout();
            docEditor.MainMenuStrip.Items.AddRange(new ToolStripItem[1]
            {
                (ToolStripItem) toolStripMenuItem
            });
            toolStripMenuItem.DropDownItems.AddRange((ToolStripItem[])this.SandboxMenuItems.ToArray());
            toolStripMenuItem.Name = "SandboxMenu";
            toolStripMenuItem.Size = new Size(125, 29);
            toolStripMenuItem.Text = "Sandbox";
            docEditor.MainMenuStrip.ResumeLayout(false);
            docEditor.MainMenuStrip.PerformLayout();
            GH_DocumentEditor.AggregateShortcutMenuItems += new GH_DocumentEditor.AggregateShortcutMenuItemsEventHandler(this.GH_DocumentEditor_AggregateShortcutMenuItems);
        }

        private void GH_DocumentEditor_AggregateShortcutMenuItems(
            object sender,
            GH_MenuShortcutEventArgs e)
        {
            e.AppendItem(_checksForUpdates);
            e.AppendItem(_downloadLastUpdate);
            e.AppendItem(_documentations);
            e.AppendItem(_exampleFile);
        }

        private List<ToolStripMenuItem> SandboxMenuItems
        {
            get
            {
                List<ToolStripMenuItem> SandboxMenuItems = new List<ToolStripMenuItem>();

                _checksForUpdates = new ToolStripMenuItem(SandboxCore.Resource.UpdatesIcon);
                _checksForUpdates.Size = new Size(265, 30);
                _checksForUpdates.Text = "Checks for updates";
                _checksForUpdates.Click += new EventHandler(_releaseHelper.ChecksForUpdates);

                _downloadLastUpdate = new ToolStripMenuItem(SandboxCore.Resource.DownloadIcon);
                _downloadLastUpdate.Size = new Size(265, 30);
                _downloadLastUpdate.Text = "Download updates";
                _downloadLastUpdate.Click += new EventHandler(_releaseHelper.DownloadRelease);

                _documentations = new ToolStripMenuItem(SandboxCore.Resource.DoumentationIcon);
                _documentations.Size = new Size(265, 30);
                _documentations.Text = "Sandbox Documentation";
                _documentations.Click += new EventHandler(_releaseHelper.SandboxDocumentation);

                _exampleFile = new ToolStripMenuItem(SandboxCore.Resource.ExampleFileIcon);
                _exampleFile.Size = new Size(265, 30);
                _exampleFile.Text = "Example file";
                _exampleFile.Click += new EventHandler(OpenExampleFile);

                SandboxMenuItems.Add(_checksForUpdates);
                SandboxMenuItems.Add(_downloadLastUpdate);
                SandboxMenuItems.Add(_documentations);
                SandboxMenuItems.Add(_exampleFile);

                return SandboxMenuItems;
            }
        }
        private void OpenExampleFile(object sender, EventArgs e)
        {
            string path = Package.GhExampleFileDir;
            if (!File.Exists(path))
            {
                MessageBox.Show($"Strange! The file should be stored here \n {path}");
            }
            var Gh = new GH_RhinoScriptInterface();
            Gh.OpenDocument(path);
        }
    }
}
