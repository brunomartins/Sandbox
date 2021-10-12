using Core;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace GhExcel
{
    public class Info : GH_AssemblyInfo
    {
        public override string Name => PackageInfo.LibraryName;

        public override Bitmap Icon => Resources.GhExcelIcon;

        public override string Description => PackageInfo.Description;

        public override Guid Id => new Guid("79B3FE74-B772-4FD5-9307-30BF3E093800");

        public override string AuthorName => PackageInfo.AuthorName;

        public override string AuthorContact => PackageInfo.AuthorContact;
    }
}