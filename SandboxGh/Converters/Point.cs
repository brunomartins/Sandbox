using GShark.Geometry;
using Rhino.Geometry;

namespace SandboxGh.Converters
{
    public static class Point
    {
        /// <summary>
        /// Converts a Rhino <see cref="Point3d"/> into a GShark <see cref="Point3"/>.
        /// </summary>
        /// <param name="pt">Rhino point.</param>
        /// <returns>GShark point.</returns>
        public static Point3 RhToGs(this Point3d pt)
        {
            return new Point3(pt.X, pt.Y, pt.Z);
        }

        /// <summary>
        /// Converts a GShark <see cref="Point3"/> into a Rhino <see cref="Point3d"/>.
        /// </summary>
        /// <param name="pt">GShark point.</param>
        /// <returns>Rhino point.</returns>
        public static Point3d GsToRh(this Point3 pt)
        {
            return new Point3d(pt.X, pt.Y, pt.Z);
        }
    }
}
