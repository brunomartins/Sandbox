using Grasshopper.Kernel;
using SandboxCore;

namespace SandboxGh.Attributes
{
    public abstract class MMComponent : GH_Component
    {
        protected MMComponent(string name, string nickname, string description, string subCategory)
            : base(name, nickname, description, PackageInfo.Category, subCategory)
        {
        }

        public override void CreateAttributes() => m_attributes = new BaseComponentAttribute(this);

        public override GH_Exposure Exposure => GH_Exposure.primary;
    }
}
