using System;
using System.Collections.Generic;
using Grasshopper.Kernel.Types;

namespace SandboxGh.Data
{
    public sealed class GH_Dict : GH_Goo<Dictionary<string, object>>, IDisposable
    {
        public GH_Dict() : base()
        {
        }

        public override string TypeName => "Dictionary";

        public override string TypeDescription => "An instance of a dictionary";

        public override bool IsValid => Value.Count > 0;

        public GH_Dict(Dictionary<string, object> dict)
        {
            Value = dict;
        }

        public GH_Dict(GH_Dict other)
        {
            Value = other.Value;
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
                Value = new Dictionary<string, object>(d);
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

        /// <summary>
        /// Convert the object to a normal dictionary.
        /// </summary>
        /// <param name="ghDict">The GH_Dict object.</param>
        /// <param name="dictResult">The Dictionary expected.</param>
        public static void ToDictionary(GH_Dict ghDict, ref Dictionary<string, object> dictResult)
        {
            foreach (KeyValuePair<string, object> kvp in ghDict.Value)
            {
                if (kvp.Value is GH_Dict nestedDict)
                {
                    Dictionary<string, object> temp = new Dictionary<string, object>();
                    ToDictionary(nestedDict, ref temp);
                    dictResult.Add(kvp.Key, temp);
                }
                else
                {
                    // Note: if we want the full serialization of the object we to remove the ToString().
                    dictResult.Add(kvp.Key, kvp.Value.ToString());
                }
            }

        }

        public void Dispose()
        {
            Value = null;
            m_value = null;
        }
    }

}
