using Grasshopper;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using SandboxCore.Utilities;
using SandboxCore.Utilities.Github;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SandboxGh.Attributes
{
    public class SandboxAssemblyPriority : GH_AssemblyPriority
    {
        private ToolStripMenuItem _checksForUpdates;
        private ToolStripMenuItem _downloadLastUpdate;
        private ToolStripMenuItem _documentations;
        private event Helper.DelEvent _gitEvents;
        private string _releaseVersion;
        private string _localVersion;

        public override GH_LoadingInstruction PriorityLoad()
        {
            Instances.ComponentServer.AddAlias("col", new Guid("03AD926B-5642-402E-88B2-14F752595928"));
            Instances.CanvasCreated += new Instances.CanvasCreatedEventHandler(this.RegisterNewMenuItems);
            _gitEvents += new Helper.DelEvent(Helper.GetLastTagRelease);
            _releaseVersion = GetReleaseVersion();
            _localVersion = Package.GetSandboxVersion(Package.DynamoDir());
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
            e.AppendItem(this._checksForUpdates);
            e.AppendItem(this._downloadLastUpdate);
            e.AppendItem(this._documentations);
        }

        private List<ToolStripMenuItem> SandboxMenuItems
        {
            get
            {
                List<ToolStripMenuItem> SandboxMenuItems = new List<ToolStripMenuItem>();

                this._checksForUpdates = new ToolStripMenuItem();
                this._checksForUpdates.Size = new Size(265, 30);
                this._checksForUpdates.Text = "Checks for updates";
                this._checksForUpdates.Click += new EventHandler(this.ChecksForUpdates);

                this._downloadLastUpdate = new ToolStripMenuItem();
                this._downloadLastUpdate.Size = new Size(265, 30);
                this._downloadLastUpdate.Text = "Download updates";
                this._downloadLastUpdate.Click += new EventHandler(this.DownloadRelease);

                this._documentations = new ToolStripMenuItem();
                this._documentations.Size = new Size(265, 30);
                this._documentations.Text = "Sandbox Documentation";
                this._documentations.Click += new EventHandler(this.GoToDocumentation);

                SandboxMenuItems.Add(this._checksForUpdates);
                SandboxMenuItems.Add(this._downloadLastUpdate);
                SandboxMenuItems.Add(this._documentations);

                return SandboxMenuItems;
            }
        }

        private string GetReleaseVersion()
        {
            var releaseVersion = _gitEvents.Invoke().Result;
            _gitEvents -= new Helper.DelEvent(Helper.GetLastTagRelease);

            if (releaseVersion.Contains("alpha"))
            {
                releaseVersion = Regex.Replace(releaseVersion, "[a-zA-Z -]", "");
            }

            return releaseVersion;
        }

        private bool IsSandboxUpdated
        {
            get
            {
                var releaseVersion = _releaseVersion;
                if (releaseVersion.Contains("alpha"))
                {
                    releaseVersion = Regex.Replace(releaseVersion, "[a-zA-Z -]", "");
                }
                return _localVersion == releaseVersion;
            }
        }

        private void GoToDocumentation(object sender, EventArgs e) => Helper.SandboxDocumentation();

        private void DownloadRelease(object sender, EventArgs e)
        {
            _gitEvents += new Helper.DelEvent(Helper.GetLastAsset);
            _ = _gitEvents.Invoke().Result;
            _gitEvents -= new Helper.DelEvent(Helper.GetLastAsset);
        }

        private void ChecksForUpdates(object sender, EventArgs e)
        {
            string message = (IsSandboxUpdated)
                ? $"Your are update to the version {_localVersion}"
                : $"Your actual version: {_localVersion}\n" +
                  $"Last release: {_releaseVersion}\n" +
                  "Alpha version meaning new features in development.";

            MessageBox.Show(message);
        }
    }
}
