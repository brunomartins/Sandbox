using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using SandboxCore.Geometry;
using SandboxDynUtilities.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.GeometryTools
{
    /// <summary>
    /// Tools operating on lines.
    /// </summary>
    public static class Lines
    {
        /// <summary>
        /// Orders lines by proximity between each others.
        /// </summary>
        /// <param name="lines">The collection of lines to order.</param>
        /// <returns name="L">A collection of lines ordered.</returns>
        /// <returns name="i">Indexes ordered.</returns>
        /// <returns name="R">Line that have to be reversed to have the point continue.</returns>
        /// <search>lines, order</search>
        [MultiReturn(new[] { "L", "i", "R" })]
        public static IDictionary RemoveCollinear(List<Line> lines)
        {
            if (lines.Count == 0)
            {
                return new Dictionary<string, object>
                {
                    {"L", lines},
                    {"i", Array.Empty<int>()},
                    {"R", Array.Empty<bool>()}
                };
            }

            var convertedLines = lines.Select(l => l.DyToGs());
            var result = LineHelper.OrderByProximity(convertedLines);

            var orderedLines = result.itemsOrdered.Select(i => lines[i]);

            return new Dictionary<string, object>
            {
                {"L", orderedLines},
                {"i", result.itemsOrdered},
                {"R", result.revers}
            };
        }
    }
}
