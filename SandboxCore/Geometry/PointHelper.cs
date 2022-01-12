using GShark.Core;
using GShark.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace SandboxCore.Geometry
{
    public static class PointHelper
    {
        public static IEnumerable<Point3> RemoveCollinear(IEnumerable<Point3> pts)
        {
            Point3[] ptsCollection = pts.ToArray();

            List<Point3> result = new List<Point3> { ptsCollection[0] };

            for (int i = 0; i < ptsCollection.Length; i++)
            {
                if (Trigonometry.ArePointsCollinear(ptsCollection[i], ptsCollection[i + 1], ptsCollection[i + 2]))
                    continue;
                result.Add(ptsCollection[i + 1]);
            }

            if (ptsCollection.Last() != ptsCollection.First())
            {
                if (Trigonometry.ArePointsCollinear(result[result.Count - 1], ptsCollection[ptsCollection.Length - 1], result[0]))
                    return result;
            }

            result.Add(ptsCollection.Last());
            return result;
        }
    }
}
