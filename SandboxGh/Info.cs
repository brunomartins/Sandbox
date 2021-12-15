using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace SandboxGh
{
    public class Info : GH_AssemblyInfo
    {
        public override string Name => "SandboxGh";

        public override Bitmap Icon => Resources.SandboxIcon;

        public override string Description => "The Grasshopper library of the Sandbox.";

        public override Guid Id => new Guid("79B3FE74-B772-4FD5-9307-30BF3E093800");

        public override string AuthorName => "Mirco Bianchini";

        public override string AuthorContact => "mirco.bianchini@mottmac.com";
    }
}