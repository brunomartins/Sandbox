using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Rhino.Geometry;
using SandboxCore.Geometry;
using SandboxGh.Attributes;
using SandboxGh.Converters;

namespace SandboxGh.Geometry
{
    public class RemoveCollinearPoints : SandboxComponent
    {
        public RemoveCollinearPoints()
            : base("Removes collinear points.", "Geometry")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "P", "Collection of points.", GH_ParamAccess.list);
            pManager.AddNumberParameter("Tolerance", "Ft", "Tolerance, deviation from the straight line (if omitted, document tolerance is used)", GH_ParamAccess.item);
            pManager[1].Optional = true;
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
            double tol = DocumentTolerance();
            DA.GetData<double>(1, ref tol);

            var convertedPts = pts.Select(pt => pt.RhToGs());
            var result = PointHelper.RemoveCollinear(convertedPts, tol).Select(pt => pt.GsToRh());

            DA.SetDataList(0, result);
        }

        protected override System.Drawing.Bitmap Icon => Resources.RemoveCollinearPoints;

        public override Guid ComponentGuid => new Guid("443216FF-3356-4E5E-BD53-602C2B35CE07");
    }
}