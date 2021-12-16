using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using Grasshopper.Kernel.Types;
using SandboxGh.Attributes;

namespace SandboxGh.UtilityTools
{
    public class CreateDictionary : SandboxComponent
    {
        private Dictionary<string, IGH_Goo> _dict;
        public CreateDictionary()
          : base("Create a new dictionary", "Utilities")
        {
        }

        public override void CreateAttributes()
        {
            m_attributes = new ButtonAttribute(this);
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Key", "K", "List of keys.", GH_ParamAccess.list);
            pManager.AddGenericParameter("Value", "V", "List of values.", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Dict", "D", "New dictionary", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var keys = new List<string>();
            var values = new List<IGH_Goo>();

            if (!DA.GetDataList(0, keys)) return;
            if (!DA.GetDataList(1, values)) return;

            if (values.Count != keys.Count)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "You must supply an equal number of keys and values.");
                return;
            }

            _dict = new Dictionary<string, IGH_Goo>();
            for (int i = 0; i < keys.Count; i++)
            {
                if (!_dict.ContainsKey(keys[i]))
                    _dict.Add(keys[i], values[i]);
                else
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, $"Key {keys[i]} at index {i} was skipped because it already existed.");
            }

            var ghDict = new GH_Dict(_dict);
            DA.SetData(0, ghDict);
        }

        protected override Bitmap Icon => Resources.CreateDictIcon;

        public override Guid ComponentGuid => new Guid("70F44B2A-AED6-493D-978D-2AE0D9E9A15C");

        /// <summary>
        /// Creates a value list pre-populated with user text keys and adds it to the Grasshopper Document, located near the component pivot.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        internal void CreateValueList(object sender, EventArgs e)
        {
            var docIO = new GH_DocumentIO { Document = new GH_Document() };

            //initialize object
            var vl = new GH_ValueList();
            //clear default contents
            vl.ListItems.Clear();
            vl.ListMode = GH_ValueListMode.DropDown;
            //add all the keys as both "Keys" and values
            foreach (var kvp in _dict)
            {
                var valueList = new GH_ValueListItem(kvp.Key, $"\"{kvp.Value.ToString()}\"");
                vl.ListItems.Add(valueList);
            }
            //set component nickname
            vl.NickName = "";
            //get active GH doc
            GH_Document doc = OnPingDocument();
            if (docIO.Document == null) return;
            // place the object
            docIO.Document.AddObject(vl, false, 1);
            //get the pivot of the keys input param
            PointF keysInputPivot = Params.Input[1].Attributes.Pivot;
            //set the pivot of the new object
            vl.Attributes.Pivot = new PointF(keysInputPivot.X - 120, keysInputPivot.Y - 11);

            docIO.Document.SelectAll();
            docIO.Document.ExpireSolution();
            docIO.Document.MutateAllIds();
            IEnumerable<IGH_DocumentObject> objs = docIO.Document.Objects;
            doc.DeselectAll();
            doc.UndoUtil.RecordAddObjectEvent("Create a value list of the dictionary", objs);
            doc.MergeDocument(docIO.Document);
        }
    }
}