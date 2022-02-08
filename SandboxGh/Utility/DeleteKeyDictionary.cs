﻿using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using SandboxGh.Attributes;

namespace SandboxGh.Utility
{
    public class DeleteKeyDictionary : SandboxComponent
    {
        public DeleteKeyDictionary()
          : base("This component allows to delete a key-value pair of a select key in a dictionary", "Utilities")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Dict", "D", "The dictionary to delete the key-value pair.", GH_ParamAccess.item);
            pManager.AddTextParameter("Key", "K", "The key to delete.", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Result dictionary", "R", "The dictionary with the removed values.", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var ghDict = new GH_Dict();
            var keys = new List<string>();

            if (!DA.GetData(0, ref ghDict) && ghDict.Value == null) return;
            if (!DA.GetDataList(1, keys)) return;

            Dictionary<string, IGH_Goo> dict = new Dictionary<string, IGH_Goo>(ghDict.Value);
            for (int i = 0; i < keys.Count; i++)
            {
                if (!dict.ContainsKey(keys[i]))
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Warning,
                        $"Key {keys[i]} at index {i} was skipped because it doesn't exist.");
                }
                else
                    dict.Remove(keys[i]);
            }

            DA.SetData(0, new GH_Dict(dict));
        }

        protected override System.Drawing.Bitmap Icon => Resources.DeleteKetDictIcon;

        public override Guid ComponentGuid => new Guid("2DD78996-31C9-44A8-A884-BB4AAC9741A1");
    }
}