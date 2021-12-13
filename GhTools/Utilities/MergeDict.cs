using Core;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using GhTools.Attributes;

namespace GhTools.Utilities
{
    public class MergeDict : GH_Component, IGH_VariableParameterComponent
    {
        public MergeDict()
          : base("Merge Dictionary", "MergeDict",
              "Merge the dictionary in one dictionary.",
              PackageInfo.Category, "Utilities")
        {
            Params.ParameterSourcesChanged +=
                new GH_ComponentParamServer.ParameterSourcesChangedEventHandler(ParamSourcesChanged);
        }

        public override void CreateAttributes()
        {
            m_attributes = new BaseCustomAttribute(this);
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), string.Empty, string.Empty, string.Empty, GH_ParamAccess.item);
            pManager.AddParameter(new DictParam(), string.Empty, string.Empty, string.Empty, GH_ParamAccess.item);

        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Merged dictionaries", "M", "The result of the merged dictionaries.", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            if (DA.Iteration > 0)
                return;

            var ghDict = new GH_Dict();
            DA.GetData(0, ref ghDict);
            if (ghDict.Value == null) return;

            Dictionary<string, IGH_Goo> mergeDictionary = new Dictionary<string, IGH_Goo>(ghDict.Value);
            int num = Params.Input.Count - 1;
            int index = 1;

            while (index <= num)
            {
                var tempGhDict = new GH_Dict();
                if (DA.GetData(0, ref tempGhDict) && tempGhDict != null)
                {
                    tempGhDict.Value.ToList().ForEach(dict => mergeDictionary.Add(dict.Key, dict.Value));
                }
                ++index;
            }

            DA.SetData(0, mergeDictionary);
        }

        protected override System.Drawing.Bitmap Icon => Resources.MergeDictIcon;

        public override GH_Exposure Exposure => GH_Exposure.primary;

        public override Guid ComponentGuid => new Guid("C809F43A-AFAB-4CC7-AF94-4F01A60CC8B0");

        public bool CanInsertParameter(GH_ParameterSide side, int index) => side == GH_ParameterSide.Input;

        public bool CanRemoveParameter(GH_ParameterSide side, int index) => side != GH_ParameterSide.Output && Params.Input.Count > 1;

        public IGH_Param CreateParameter(GH_ParameterSide side, int index) => new Param_GenericObject();

        public bool DestroyParameter(GH_ParameterSide side, int index) => true;

        public void VariableParameterMaintenance()
        {
            int num = checked(Params.Input.Count - 1);
            int index = 0;
            while (index <= num)
            {
                Params.Input[index].Name = $"Dictionary {(object)checked(index + 1)}";
                Params.Input[index].NickName = $"D{(object)checked(index + 1)}";
                Params.Input[index].Description = $"Dictionary stream {(object)checked(index + 1)}";
                Params.Input[index].Optional = true;
                Params.Input[index].MutableNickName = false;
                Params.Input[index].Access = GH_ParamAccess.tree;
                ++index;
            }
        }

        private void ParamSourcesChanged(object sender, GH_ParamServerEventArgs e)
        {
            if (e.ParameterSide != GH_ParameterSide.Input || e.ParameterIndex != checked(Params.Input.Count - 1) || e.Parameter.SourceCount <= 0)
                return;
            Params.RegisterInputParam(this.CreateParameter(GH_ParameterSide.Input, Params.Input.Count));
            VariableParameterMaintenance();
            Params.OnParametersChanged();
        }
    }
}