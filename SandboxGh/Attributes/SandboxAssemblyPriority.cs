using Grasshopper;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SandboxGh.Attributes
{
    public class SandboxAssemblyPriority : GH_AssemblyPriority
    {
        private ToolStripMenuItem _sayHelloSandbox;

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
        }

        private List<ToolStripMenuItem> SandboxMenuItems
        {
            get
            {
                List<ToolStripMenuItem> SandboxMenuItems = new List<ToolStripMenuItem>();
                this._sayHelloSandbox = new ToolStripMenuItem();

                this._sayHelloSandbox.Name = "sayHelloSandbox";
                this._sayHelloSandbox.Size = new Size(265, 30);
                this._sayHelloSandbox.Text = "Hello Sandbox";
                this._sayHelloSandbox.Click += new EventHandler(this.SayHello);
                
                SandboxMenuItems.Add(this._sayHelloSandbox);

                return SandboxMenuItems;
            }
        }

        private void SayHello(object sender, EventArgs e) => MessageBox.Show("CIAO BELLO!");
    }
}
