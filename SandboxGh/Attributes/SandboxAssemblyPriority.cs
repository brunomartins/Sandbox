using Grasshopper;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using SandboxCore.Utilities.Github;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SandboxCore.Utilities;

namespace SandboxGh.Attributes
{
    public class SandboxAssemblyPriority : GH_AssemblyPriority
    {
        private ToolStripMenuItem _sandboxVersion;
        private ToolStripMenuItem _sandboxDocumentation;
        private event Helper.DelEvent _gitEvents;
        private string _releaseVersion;
        private bool _isUpdated;

        public override GH_LoadingInstruction PriorityLoad()
        {
            Instances.ComponentServer.AddAlias("col", new Guid("03AD926B-5642-402E-88B2-14F752595928"));
            Instances.CanvasCreated += new Instances.CanvasCreatedEventHandler(this.RegisterNewMenuItems);
            _gitEvents += new Helper.DelEvent(Helper.GetReleaseVersion);
            _releaseVersion = GetReleaseVersion();
            _isUpdated = IsSandboxUpdated(_releaseVersion);
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
            toolStripMenuItem.BackColor = (_isUpdated)
                ? Control.DefaultBackColor
                : Color.FromArgb((int)byte.MaxValue, 231, 47, 135);
            docEditor.MainMenuStrip.ResumeLayout(false);
            docEditor.MainMenuStrip.PerformLayout();
            GH_DocumentEditor.AggregateShortcutMenuItems += new GH_DocumentEditor.AggregateShortcutMenuItemsEventHandler(this.GH_DocumentEditor_AggregateShortcutMenuItems);
        }

        private void GH_DocumentEditor_AggregateShortcutMenuItems(
            object sender,
            GH_MenuShortcutEventArgs e)
        {
            e.AppendItem(this._sandboxVersion);
            e.AppendItem(this._sandboxDocumentation);
        }

        private List<ToolStripMenuItem> SandboxMenuItems
        {
            get
            {
                List<ToolStripMenuItem> SandboxMenuItems = new List<ToolStripMenuItem>();

                this._sandboxVersion = new ToolStripMenuItem();
                this._sandboxVersion.Size = new Size(265, 30);
                this._sandboxVersion.Text = (_isUpdated)
                    ? "Sandbox is updated to the last version!"
                    : $"New version is available: {_releaseVersion}";

                this._sandboxDocumentation = new ToolStripMenuItem();
                this._sandboxDocumentation.Size = new Size(265, 30);
                this._sandboxDocumentation.Text = "Sandbox Documentation";
                this._sandboxDocumentation.Click += new EventHandler(this.GoToDocumentation);

                SandboxMenuItems.Add(this._sandboxVersion);
                SandboxMenuItems.Add(this._sandboxDocumentation);

                return SandboxMenuItems;
            }
        }

        private string GetReleaseVersion()
        {
            var releaseVersion = _gitEvents.Invoke().Result;
            _gitEvents -= new Helper.DelEvent(Helper.GetReleaseVersion);

            if (releaseVersion.Contains("alpha"))
            {
                releaseVersion = Regex.Replace(releaseVersion, "[a-zA-Z -]", "");
            }

            return releaseVersion;
        }

        private bool IsSandboxUpdated(string releaseVersion)
        {
            var installedVersion = Package.GetSandboxVersion(Package.DynamoDir());

            if (releaseVersion.Contains("alpha"))
            {
                releaseVersion = Regex.Replace(releaseVersion, "[a-zA-Z -]", "");
            }

            return installedVersion == releaseVersion;
        }

        private void GoToDocumentation(object sender, EventArgs e) => Helper.SandboxDocumentation();
    }
}
