using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using SandboxGh.Attributes;

namespace SandboxGh.UtilityTools
{
    public class MergeDictionary : SandboxComponent, IGH_VariableParameterComponent
    {
        public MergeDictionary()
          : base("Merge the dictionary in one dictionary.", "Utilities")
        {
            Params.ParameterSourcesChanged +=
                new GH_ComponentParamServer.ParameterSourcesChangedEventHandler(ParamSourcesChanged);
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), string.Empty, string.Empty, string.Empty, GH_ParamAccess.tree);
            pManager.AddParameter(new DictParam(), string.Empty, string.Empty, string.Empty, GH_ParamAccess.tree);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new DictParam(), "Merged dictionaries", "M", "The result of the merged dictionaries.", GH_ParamAccess.tree);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            if (DA.Iteration > 0)
                return;

            GH_Structure<GH_Dict> mergedTree = new GH_Structure<GH_Dict>();
            int num = Params.Input.Count - 1;
            int index = 0;
            while (index <= num)
            {
                if (DA.GetDataTree<GH_Dict>(index, out var treeTemp) && treeTemp != null)
                {
                    mergedTree.MergeStructure(treeTemp);
                }
                ++index;
            }

            GH_Structure<GH_Dict> result = new GH_Structure<GH_Dict>();

            for (int i = 0; i < mergedTree.Branches.Count; i++)
            {
                var branch = (IList<GH_Dict>)mergedTree.get_Branch(i);
                var path = mergedTree.get_Path(i);
                Dictionary<string, IGH_Goo> tempMergedDict = new Dictionary<string, IGH_Goo>();

                foreach (var kvp in branch.SelectMany(ghGoo => ghGoo.Value))
                {
                    if (!tempMergedDict.ContainsKey(kvp.Key))
                    {
                        tempMergedDict.Add(kvp.Key, kvp.Value);
                    }
                    else
                        AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, $"Key {kvp.Key} was skipped because it already existed.");
                }

                result.Append(new GH_Dict(tempMergedDict), path);
            }

            DA.SetDataTree(0, result);
        }

        protected override System.Drawing.Bitmap Icon => Resources.MergeDictIcon;

        public override Guid ComponentGuid => new Guid("C809F43A-AFAB-4CC7-AF94-4F01A60CC8B0");

        public bool CanInsertParameter(GH_ParameterSide side, int index) => side == GH_ParameterSide.Input;

        public bool CanRemoveParameter(GH_ParameterSide side, int index) => side != GH_ParameterSide.Output && Params.Input.Count > 1;

        public IGH_Param CreateParameter(GH_ParameterSide side, int index) => new DictParam();

        public bool DestroyParameter(GH_ParameterSide side, int index) => true;

        public void VariableParameterMaintenance()
        {
            int num = Params.Input.Count - 1;
            int index = 0;
            while (index <= num)
            {
                Params.Input[index].Name = $"Dictionary {index + 1}";
                Params.Input[index].NickName = $"D{index + 1}";
                Params.Input[index].Description = $"Dictionary stream {index + 1}";
                Params.Input[index].Optional = true;
                Params.Input[index].MutableNickName = false;
                Params.Input[index].Access = GH_ParamAccess.tree;
                ++index;
            }
        }

        private void ParamSourcesChanged(object sender, GH_ParamServerEventArgs e)
        {
            if (e.ParameterSide != GH_ParameterSide.Input || e.ParameterIndex != Params.Input.Count - 1 || e.Parameter.SourceCount <= 0)
                return;
            Params.RegisterInputParam(CreateParameter(GH_ParameterSide.Input, Params.Input.Count));
            VariableParameterMaintenance();
            Params.OnParametersChanged();
        }
    }
}