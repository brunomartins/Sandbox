using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SandboxGh.Attributes;
using GH_IO.Serialization;

namespace SandboxGh.Utility
{
    public class SortByKeyDictionary : SandboxComponent
    {
        private SortByKeyDictionary.SortingMode _sortingMode;

        public SortByKeyDictionary()
          : base("Sort the keys of a dictionary with a variety of patterns.", "Utilities")
        {
            this._sortingMode = SortByKeyDictionary.SortingMode.Ascending;
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Dict", "D", "Dictionary", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Sorted dictionary", "S", "The dictionary result after being sorted.", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var ghDict = new GH_Dict();
            if (!DA.GetData(0, ref ghDict)) return;
            switch (this._sortingMode)
            {
                case SortByKeyDictionary.SortingMode.None:
                    this.Message = (string)null;
                    DA.SetData(0, ghDict);
                    break;
                case SortByKeyDictionary.SortingMode.Ascending:
                    this.Message = "Ascending";
                    var sortedDict = ghDict.Value.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                    DA.SetData(0, sortedDict);
                    break;
                case SortByKeyDictionary.SortingMode.Descending:
                    this.Message = "Descending";
                    var revSortedDict = ghDict.Value.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                    DA.SetData(0, revSortedDict);
                    break;
            }
        }
        protected override void AppendAdditionalComponentMenuItems(ToolStripDropDown menu)
        {
            ToolStripMenuItem toolStripMenuItem1 = GH_DocumentObject.Menu_AppendItem((ToolStrip)menu, "Ascending", new EventHandler(this.Menu_AscendingClicked), true, this._sortingMode == SortByKeyDictionary.SortingMode.Ascending);
            ToolStripMenuItem toolStripMenuItem2 = GH_DocumentObject.Menu_AppendItem((ToolStrip)menu, "Descending", new EventHandler(this.Menu_DescendingClicked), true, this._sortingMode == SortByKeyDictionary.SortingMode.Descending);
               toolStripMenuItem1.ToolTipText = "Sorts in Ascending order";
            toolStripMenuItem2.ToolTipText = "Sorts in Descending order";
        }

        private void Menu_AscendingClicked(object sender, EventArgs e)
        {
            if (this._sortingMode == SortByKeyDictionary.SortingMode.Ascending)
                return;
            this.RecordUndoEvent("Ascending");
            this._sortingMode = SortByKeyDictionary.SortingMode.Ascending;
            this.ExpireSolution(true);
        }
        private void Menu_DescendingClicked(object sender, EventArgs e)
        {
            if (this._sortingMode == SortByKeyDictionary.SortingMode.Descending)
                return;
            this.RecordUndoEvent("Descending");
            this._sortingMode = SortByKeyDictionary.SortingMode.Descending;
            this.ExpireSolution(true);
        }
        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("SortingMode", (int)this._sortingMode);
            return base.Write(writer);
        }
        public override bool Read(GH_IReader reader)
        {
            int num = 1;
            reader.TryGetInt32("SortingMode", ref num);
            this._sortingMode = (SortByKeyDictionary.SortingMode)num;
            return base.Read(reader);
        }
        private enum SortingMode
        {
            None,
            Ascending,
            Descending,
        }
        protected override System.Drawing.Bitmap Icon => Resources.SortByKeyDictIcon;
        public override Guid ComponentGuid => new Guid("FD0E8696-15EA-4E06-9182-D4569C830AB2");
    }
}