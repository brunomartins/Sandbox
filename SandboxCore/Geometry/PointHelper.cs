using System;
using GShark.Core;
using GShark.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace SandboxCore.Geometry
{
    /// <summary>
    /// Collections of methods used to operate in points.
    /// </summary>
    public static class PointHelper
    {
        /// <summary>
        /// Remove the collinear points into a ordered collection of points.
        /// </summary>
        /// <param name="pts">Collection of point to clean up.</param>
        /// <param name="tol">Tolerance from the straight line between collinear points. Set by default at 1e-3.</param>
        /// <returns>Cleaned collection of points.</returns>
        public static IEnumerable<Point3> RemoveCollinear(IEnumerable<Point3> pts, double tol = 1e-3)
        {
            Point3[] ptsCollection = pts.ToArray();

            List<Point3> result = new List<Point3> { ptsCollection[0] };

            for (int i = 0; i < ptsCollection.Length - 2; i++)
            {
                if (ArePointsCollinear(ptsCollection[i], ptsCollection[i + 1], ptsCollection[i + 2], tol))
                    continue;
                result.Add(ptsCollection[i + 1]);
            }

            if (ptsCollection.Last() != ptsCollection.First())
            {
                if (ArePointsCollinear(result[result.Count - 1], ptsCollection[ptsCollection.Length - 1], result[0], tol))
                    return result;
            }

            result.Add(ptsCollection.Last());
            return result;
        }

        /// <summary>
        /// Determines if three points form a straight line (are collinear) within a given tolerance.<br/>
        /// Find the deviation from the straight line.<br/>
        /// </summary>
        /// <param name="pt1">First point.</param>
        /// <param name="pt2">Second point.</param>
        /// <param name="pt3">Third point.</param>
        /// <param name="tol">Tolerance set per default as 1e-3</param>
        /// <returns>True if the three points are collinear.</returns>
        private static bool ArePointsCollinear(Point3 pt1, Point3 pt2, Point3 pt3, double tol = 1e-3)
        {
            Vector3 pt1ToPt2 = pt2 - pt1;
            Vector3 pt1ToPt3 = pt3 - pt1;
            if (pt1ToPt2.IsZero || pt1ToPt3.IsZero)
            {
                return true;
            }
            double angle = Vector3.VectorAngle(pt1ToPt2, pt1ToPt3);
            double deviation = Math.Sin(angle) * pt1ToPt2.Length;

            return Math.Abs(deviation - GSharkMath.MinTolerance) < tol;
        }
    }
}
