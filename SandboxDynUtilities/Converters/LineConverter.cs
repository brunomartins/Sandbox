using Autodesk.DesignScript.Runtime;
using GShark.Geometry;
using Ad = Autodesk.DesignScript.Geometry;

namespace SandboxDynUtilities.Converters
{
    [IsVisibleInDynamoLibrary(false)]
    public static class LineConverter
    {
        /// <summary>
        /// Converts a Dynamo <see cref="Ad.Line"/> into a GShark <see cref="Line"/>.
        /// </summary>
        /// <param name="line">Dynamo line.</param>
        /// <returns>GShark line.</returns>
        public static Line DyToGs(this Ad.Line line)
        {
            return new Line(line.StartPoint.DyToGs(), line.EndPoint.DyToGs());
        }

        /// <summary>
        /// Converts a GShark <see cref="Line"/> into a Dynamo <see cref="Ad.Line"/>.
        /// </summary>
        /// <param name="line">GShark point.</param>
        /// <returns>Dynamo line.</returns>
        public static Ad.Line GsToDy(this Line line)
        {
            return Ad.Line.ByStartPointEndPoint(line.StartPoint.GsToDy(), line.EndPoint.GsToDy());
        }
    }
}
