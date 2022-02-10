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
    public class SortingDictionary : SandboxComponent
    {

        //Create the _sortingMode property
        private SortingDictionary.SortingMode _sortingMode;

        //Set description as well as assign default starting value to _sortingMode (Ascending)
        public SortingDictionary()
          : base("Sort the keys of a dictionary with a variety of patterns.", "Utilities")
        {
            this._sortingMode = SortingDictionary.SortingMode.Ascending;
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
                case SortingDictionary.SortingMode.None:
                    DA.SetData(0, ghDict);
                    this.Message = (string)null;
                    break;

                case SortingDictionary.SortingMode.Ascending:
                    // return the dictionary sorted in ascending mode
                    var sortedDict = new Dictionary<string, IGH_Goo>();
                    foreach (var item in ghDict.Value.OrderBy(x => x.Key))
                    {
                        sortedDict.Add(item.Key, item.Value);
                    }
                    DA.SetData(0, sortedDict);
                    this.Message = "Ascending";
                    break;

                case SortingDictionary.SortingMode.Descending:
                    // return the dictionary sorted in descending mode
                    
                    var revSortedDict = new Dictionary<string, IGH_Goo>();
                    foreach (var item in ghDict.Value.OrderByDescending(x => x.Key))
                    {
                        revSortedDict.Add(item.Key, item.Value);
                    }
                    DA.SetData(0, revSortedDict);
                    this.Message = "Descending";
                    break;

            }
        }
            
        protected override void AppendAdditionalComponentMenuItems(ToolStripDropDown menu)
        {
            ToolStripMenuItem toolStripMenuItem1 = GH_DocumentObject.Menu_AppendItem((ToolStrip)menu, "Ascending", new EventHandler(this.Menu_AscendingClicked), true, this._sortingMode == SortingDictionary.SortingMode.Ascending);
            ToolStripMenuItem toolStripMenuItem2 = GH_DocumentObject.Menu_AppendItem((ToolStrip)menu, "Descending", new EventHandler(this.Menu_DescendingClicked), true, this._sortingMode == SortingDictionary.SortingMode.Descending);
               toolStripMenuItem1.ToolTipText = "Sorts in Ascending order";
            toolStripMenuItem2.ToolTipText = "Sorts in Descending order";
        }




        //Change the _sortingModed when the menu gets clicked
        private void Menu_AscendingClicked(object sender, EventArgs e)
        {
            //If the same one is clicked, just return
            if (this._sortingMode == SortingDictionary.SortingMode.Ascending)
                return;
            this.RecordUndoEvent("Ascending");
            //Otherwise actually set this._sortingMode to its new value
            this._sortingMode = SortingDictionary.SortingMode.Ascending;
            this.ExpireSolution(true);
        }
        private void Menu_DescendingClicked(object sender, EventArgs e)
        {
            if (this._sortingMode == SortingDictionary.SortingMode.Descending)
                return;
            this.RecordUndoEvent("Descending");
            this._sortingMode = SortingDictionary.SortingMode.Descending;
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
            this._sortingMode = (SortingDictionary.SortingMode)num;
            return base.Read(reader);
        }



        private enum SortingMode
        {
            None,
            Ascending,
            Descending,
        }

        protected override System.Drawing.Bitmap Icon => Resources.SortDictIcon;

        public override Guid ComponentGuid => new Guid("FD0E8696-15EA-4E06-9182-D4569C830AB2");
    }
}