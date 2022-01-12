using GShark.Core;
using GShark.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.GeometryHelper
{
    public static class PointsHelper
    {
        public static IEnumerable<Point3> RemoveCollinearPoints(IEnumerable<Point3> pts)
        {
            var ptsConverted = pts.ToList();
            if (!ptsConverted.Any()) return new List<Point3>();

            List<Point3> ptsResult = new List<Point3> { ptsConverted[0] };
            for (int i = 0; i < ptsConverted.Count; i++)
            {
                if (!Trigonometry.ArePointsCollinear(ptsConverted[i], ptsConverted[i + 1], ptsConverted[i + 2]))
                {
                    ptsResult.Add(ptsConverted[i + 1]);
                }
            }

            return ptsResult;
        }
    }
}
