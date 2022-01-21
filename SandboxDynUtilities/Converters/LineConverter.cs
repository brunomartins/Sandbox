using Autodesk.DesignScript.Runtime;
using GShark.Geometry;
using Ad = Autodesk.DesignScript.Geometry;

namespace SandboxDynUtilities.Converters
{
    [IsVisibleInDynamoLibrary(false)]
    public static class LineConverter
    {
        public static Line DyToGs(this Ad.Line line)
        {
            return new Line(line.StartPoint.DyToGs(), line.EndPoint.DyToGs());
        }

        public static Ad.Line GsToDy(this Line line)
        {
            return Ad.Line.ByStartPointEndPoint(line.StartPoint.GsToDy(), line.EndPoint.GsToDy());
        }
    }
}
