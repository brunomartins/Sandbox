using System.Collections.Generic;
using System.Collections.ObjectModel;
using Grasshopper.Kernel.Types;

namespace SandboxGh.Utility
{
    public sealed class GH_Dict : GH_Goo<ReadOnlyDictionary<string, object>>
    {
        public GH_Dict()
        {
            Value = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());
        }

        public override string TypeName => "Dictionary";

        public override string TypeDescription => "An instance of a dictionary";

        public override bool IsValid => Value.Count > 0;

        public GH_Dict(IDictionary<string, object> dict)
        {
            Value = new ReadOnlyDictionary<string, object>(dict);
        }

        public GH_Dict(GH_Dict other)
        {
            Value = new ReadOnlyDictionary<string, object>(other.Value);
        }

        public override IGH_Goo Duplicate() =>
            new GH_Dict(this);

        public override string ToString() => $"Dict: [{Value.Count}]";

        public override object ScriptVariable() => Value;

        public override bool CastFrom(object source)
        {
            if (source == null) { return false; }

            if (source is IDictionary<string, object> d)
            {
                Value = new ReadOnlyDictionary<string, object>(d);
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
