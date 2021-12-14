using Core;
using GhTools.Attributes;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GhTools.UtilityTools
{
    public class DictParam : GH_Param<GH_Dict>
    {
        public DictParam()
            : base("Dictionary", "Dict",
                "Dictionary parameter",
                PackageInfo.Category, "Utilities", GH_ParamAccess.item)
        {
        }

        public override void CreateAttributes()
        {
            m_attributes = new BaseParamAttributes(this);
        }

        protected override string VolatileDataDescription()
        {
            var maxLength = 10;
            var sb = new StringBuilder();
            sb.AppendLine(base.VolatileDataDescription());
            var dataEnum = VolatileData.AllData(true).GetEnumerator();
            var data = new List<GH_Dict>();
            while (dataEnum.MoveNext())
                data.Add((GH_Dict)dataEnum.Current);
            if (data.Count == 0 || data.Count > 1) return sb.ToString();
            var ghDict = data[0];
            if (ghDict == null) return sb.ToString();
            var dict = ghDict.Value;
            var lines = dict
                .Take(Math.Min(maxLength, dict.Count))
                .Select(kvp => $" {kvp.Key} : {kvp.Value}\n")
                .Aggregate((a, b) => a + b);
            sb.Append(lines);
            if (dict.Count > maxLength)
                sb.Append(" …");
            return sb.ToString();
        }

        protected override Bitmap Icon => Resources.DictParameterIcon;

        public override GH_Exposure Exposure => GH_Exposure.septenary | GH_Exposure.obscure;

        public override Guid ComponentGuid => new Guid("A1B6CAC4-8224-4F60-90C3-46B031C2C0EA");
    }
}
