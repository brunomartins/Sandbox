using Autodesk.DesignScript.Geometry;
using Sandbox.Converters;
using SandboxCore.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.GeometryTools
{
    /// <summary>
    /// Tools operating on points.
    /// </summary>
    public static class Points
    {
        /// <summary>
        /// Removes collinear points.
        /// </summary>
        /// <param name="pts">The collection of points that have to be check.</param>
        /// <param name="tol">Tolerance, the deviation from the straight line, if omitted, 0.001 is used.</param>
        /// <returns>A collection of points not collinear.</returns>
        /// <search>collinear, points</search>
        public static IEnumerable<Point> RemoveCollinear(List<Point> pts, double tol = 1e-3)
        {
            if (pts.Count == 0) return pts;
            var convertedPts = pts.Select(pt => pt.DyToGs());
            return PointHelper.RemoveCollinear(convertedPts, tol).Select(pt => pt.GsToDy());
        }
    }
}
