using Grasshopper.Kernel;
using SandboxGh.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;
using SandboxGh.Converters;

namespace SandboxGh.GeometryTools
{
    public class RemoveCollinearPoints : SandboxComponent
    {
        public RemoveCollinearPoints()
            : base("Remove collinear points.", "Geometry")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "P", "Collection of points.", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_PointParam("Points filtered", "R", "Cleaned points", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> pts = new List<Point3d>();
            if (!DA.GetDataList<Point3d>(0, pts)) return;
            if (pts.Count == 0) DA.SetDataList(0, pts);

            var convertedPts = pts.Select(pt => pt.RhToGs());

        }

        protected override System.Drawing.Bitmap Icon => null;

        public override Guid ComponentGuid => new Guid("443216FF-3356-4E5E-BD53-602C2B35CE07");
    }
}