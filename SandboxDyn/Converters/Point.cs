using GShark.Geometry;
using Dy = Autodesk.DesignScript.Geometry;

namespace Sandbox.Converters
{
    static class Point
    {
        /// <summary>
        /// Converts a Dynamo <see cref="Point"/> into a GShark <see cref="Point3"/>.
        /// </summary>
        /// <param name="pt">Dynamo point.</param>
        /// <returns>GShark point.</returns>
        private static Point3 DyToGs(this Dy.Point pt)
        {
            return new Point3(pt.X, pt.Y, pt.Z);
        }

        /// <summary>
        /// Converts a GShark <see cref="Point3"/> into a Dynamo <see cref="Dy.Point"/>.
        /// </summary>
        /// <param name="pt">GShark point.</param>
        /// <returns>Dynamo point.</returns>
        private static Dy.Point GsToDy(this Point3 pt)
        {
            return Dy.Point.ByCoordinates(pt.X, pt.Y, pt.Z);
        }
    }
}
