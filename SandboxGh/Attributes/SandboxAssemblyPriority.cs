using Grasshopper;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SandboxCore.Utilities.Github;

namespace SandboxGh.Attributes
{
    public class SandboxAssemblyPriority : GH_AssemblyPriority
    {
        private ToolStripMenuItem _sandboxVersion;
        private ToolStripMenuItem _sandboxDocu;

        public override GH_LoadingInstruction PriorityLoad()
        {
            Instances.ComponentServer.AddAlias("col", new Guid("03AD926B-5642-402E-88B2-14F752595928"));
            Instances.CanvasCreated += new Instances.CanvasCreatedEventHandler(this.RegisterNewMenuItems);
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
            e.AppendItem(this._sandboxVersion);
            e.AppendItem(this._sandboxDocu);
        }

        private List<ToolStripMenuItem> SandboxMenuItems
        {
            get
            {
                List<ToolStripMenuItem> SandboxMenuItems = new List<ToolStripMenuItem>();
                this._sandboxVersion = new ToolStripMenuItem();
                this._sandboxVersion.Size = new Size(265, 30);
                this._sandboxVersion.Text = "Sandbox Version";
                this._sandboxVersion.Click += new EventHandler(this.GetVersion);
                SandboxMenuItems.Add(this._sandboxVersion);

                this._sandboxDocu = new ToolStripMenuItem();
                this._sandboxDocu.Size = new Size(265, 30);
                this._sandboxDocu.Text = "Sandbox Documentation";
                this._sandboxDocu.Click += new EventHandler(this.GoToDocumentation);
                SandboxMenuItems.Add(this._sandboxDocu);

                return SandboxMenuItems;
            }
        }

        private void GetVersion(object sender, EventArgs e)
        {
            MessageBox.Show(Helper.GetReleaseVersion().Result);
        }

        private void GoToDocumentation(object sender, EventArgs e) => Helper.SandboxDocumentation();
    }
}
