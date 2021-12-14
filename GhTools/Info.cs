using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace GhTools
{
    public class Info : GH_AssemblyInfo
    {
        public override string Name => "GhTools";

        public override Bitmap Icon => Resources.SandboxIcon;

        public override string Description => "The Grasshopper library of the MMLib.";

        public override Guid Id => new Guid("79B3FE74-B772-4FD5-9307-30BF3E093800");

        public override string AuthorName => "Mirco Bianchini";

        public override string AuthorContact => "mirco.bianchini@mottmac.com";
    }
}