using Grasshopper.Kernel.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GhTools.Utilities
{
    public sealed class GH_Dict : GH_Goo<ReadOnlyDictionary<string, IGH_Goo>>
    {
        public GH_Dict()
        {
            Value = new ReadOnlyDictionary<string, IGH_Goo>(new Dictionary<string, IGH_Goo>());
        }

        public override string TypeName => "Dictionary";

        public override string TypeDescription => "An instance of a dictionary";

        public override bool IsValid => Value.Count > 0;

        public GH_Dict(IDictionary<string, IGH_Goo> dict)
        {
            Value = new ReadOnlyDictionary<string, IGH_Goo>(dict);
        }

        public GH_Dict(GH_Dict other)
        {
            Value = new ReadOnlyDictionary<string, IGH_Goo>(other.Value);
        }

        public override IGH_Goo Duplicate() =>
            new GH_Dict(this);

        public override string ToString() => $"Dict: [{Value.Count}]";

        public override object ScriptVariable() => Value;

        public override bool CastFrom(object source)
        {
            if (source == null) { return false; }

            if (source is IDictionary<string, IGH_Goo> d)
            {
                Value = new ReadOnlyDictionary<string, IGH_Goo>(d);
                return true;
            }

            return false;
        }

        public override bool CastTo<Q>(ref Q target)
        {
            if (typeof(Q).IsAssignableFrom(typeof(GH_Integer)))
            {
                object ptr = new GH_Integer(Value.Count);
                target = (Q)ptr;
                return true;
            }

            if (typeof(Q).IsAssignableFrom(typeof(GH_String)))
            {
                object ptr = new GH_String("This is a dictionary!");
                target = (Q)ptr;
                return true;
            }

            return base.CastTo(ref target);
        }

    }

}
