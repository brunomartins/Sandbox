using GShark.Geometry;
using Rh = Rhino.Geometry;

namespace SandboxGh.Converters
{
    static class LineConverter
    {
        /// <summary>
        /// Converts a Rhino <see cref="Rh.Line"/> into a GShark <see cref="Line"/>.
        /// </summary>
        /// <param name="line">Rhino line.</param>
        /// <returns>GShark line.</returns>
        internal static Line RhToGs(this Rh.Line line)
        {
            return new Line(line.From.RhToGs(), line.To.RhToGs());
        }

        /// <summary>
        /// Converts a GShark <see cref="Line"/> into a Rhino <see cref="Rh.Line"/>.
        /// </summary>
        /// <param name="line">GShark line.</param>
        /// <returns>Rhino line.</returns>
        internal static Rh.Line GsToRh(this Line line)
        {
            return new Rh.Line(line.StartPoint.GsToRh(), line.EndPoint.GsToRh());
        }
    }
}
