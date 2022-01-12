using GShark.Geometry;
using Autodesk.DesignScript.Geometry;

namespace Sandbox.Converters
{
    static class PointConverter
    {
        /// <summary>
        /// Converts a Dynamo <see cref="PointConverter"/> into a GShark <see cref="Point3"/>.
        /// </summary>
        /// <param name="pt">Dynamo point.</param>
        /// <returns>GShark point.</returns>
        internal static Point3 DyToGs(this Point pt)
        {
            return new Point3(pt.X, pt.Y, pt.Z);
        }

        /// <summary>
        /// Converts a GShark <see cref="Point3"/> into a Dynamo <see cref="Point"/>.
        /// </summary>
        /// <param name="pt">GShark point.</param>
        /// <returns>Dynamo point.</returns>
        internal static Point GsToDy(this Point3 pt)
        {
            return Point.ByCoordinates(pt.X, pt.Y, pt.Z);
        }
    }
}
