using Grasshopper.Kernel;
using SandboxCore;

namespace SandboxGh.Attributes
{
    public abstract class SandboxComponent : GH_Component
    {
        protected SandboxComponent(string description, string subCategory)
            : base("SandboxComponent", "SandboxComponent", description, PackageInfo.Category, subCategory)
        {
            base.Name = base.NickName = GetType().Name;
        }

        public override void CreateAttributes() => m_attributes = new ComponentAttribute(this);

        public override GH_Exposure Exposure => GH_Exposure.primary;
    }
}
