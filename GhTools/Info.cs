using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace GhTools
{
    public class Info : GH_AssemblyInfo
    {
        public override string Name => "MMLib";

        public override Bitmap Icon => Resources.GhExcelIcon;

        public override string Description => "The grasshopper library of the MMLib.";

        public override Guid Id => new Guid("79B3FE74-B772-4FD5-9307-30BF3E093800");

        public override string AuthorName => "Mirco Bianchini";

        public override string AuthorContact => "mirco.bianchini@mottmac.com";
    }
}