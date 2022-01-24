using GShark.Geometry;
using System.Collections.Generic;
using System.Linq;
using Array = System.Array;

namespace SandboxCore.Geometry
{
    /// <summary>
    /// Collection of methods used to operate on Lines.
    /// </summary>
    public static class LineHelper
    {
        public static (int[] itemsOrdered, bool[] revers) OrderByProximity(IEnumerable<Line> lines)
        {
            Line[] copyLines = lines.ToArray();
            int[] indexes = new int[copyLines.Length];
            bool[] revers = new bool[copyLines.Length];

            for (int j = 0; j < copyLines.Length; j++)
            {
                indexes[j] = j;
                revers[j] = false;
            }

            Point3 startPt, endPt, tempPt;
            double startDist, endDist, tempDist;
            int nthI, startNthI, startEnd, endNthI, endEnd, i;

            for (nthI = 1; nthI < copyLines.Length; nthI++)
            {
                startNthI = endNthI = nthI;
                startEnd = endEnd = 0;
                startPt = (revers[0]) ? copyLines[indexes[0]].EndPoint : copyLines[indexes[0]].StartPoint;
                endPt = (revers[nthI - 1]) ? copyLines[indexes[nthI - 1]].StartPoint : copyLines[indexes[nthI - 1]].EndPoint;
                startDist = startPt.DistanceTo(copyLines[indexes[startNthI]].StartPoint);
                endDist = endPt.DistanceTo(copyLines[indexes[endNthI]].EndPoint);

                for (i = nthI; i < copyLines.Length; i++)
                {
                    tempPt = copyLines[indexes[i]].StartPoint;

                    for (int end = 0; end < 2; end++)
                    {
                        tempDist = startPt.DistanceTo(tempPt);
                        if (tempDist < startDist)
                        {
                            startNthI = i;
                            startEnd = end;
                            startDist = tempDist;
                        }

                        tempDist = endPt.DistanceTo(tempPt);
                        if (tempDist < endDist)
                        {
                            endNthI = i;
                            endEnd = end;
                            endDist = tempDist;
                        }

                        tempPt = copyLines[indexes[i]].EndPoint;
                    }

                }

                if (startDist < endDist)
                {
                    i = indexes[nthI];
                    indexes[nthI] = indexes[startNthI];
                    indexes[startNthI] = i;
                    startNthI = indexes[nthI];
                    for (i = nthI; i > 0; i--)
                    {
                        indexes[i] = indexes[i - 1];
                        revers[i] = revers[i - 1];
                    }

                    indexes[0] = startNthI;
                    revers[0] = (startEnd != 1);
                }
                else
                {
                    i = indexes[nthI];
                    indexes[nthI] = indexes[endNthI];
                    indexes[endNthI] = i;
                    revers[nthI] = (endEnd == 1);
                }
            }

            return (indexes, revers);
        }
    }
}
