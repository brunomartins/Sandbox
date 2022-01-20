using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using GShark.Geometry;

namespace SandboxDynUtilities.Converters
{
    [IsVisibleInDynamoLibrary(false)]
    public static class PointConverter
    {
        /// <summary>
        /// Converts a Dynamo <see cref="PointConverter"/> into a GShark <see cref="Point3"/>.
        /// </summary>
        /// <param name="pt">Dynamo point.</param>
        /// <returns>GShark point.</returns>
        public static Point3 DyToGs(this Point pt)
        {
            return new Point3(pt.X, pt.Y, pt.Z);
        }

        /// <summary>
        /// Converts a GShark <see cref="Point3"/> into a Dynamo <see cref="Point"/>.
        /// </summary>
        /// <param name="pt">GShark point.</param>
        /// <returns>Dynamo point.</returns>
        public static Point GsToDy(this Point3 pt)
        {
            return Point.ByCoordinates(pt.X, pt.Y, pt.Z);
        }
    }
}
