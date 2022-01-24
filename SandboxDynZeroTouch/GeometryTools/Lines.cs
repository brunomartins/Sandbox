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
        /// <returns name="Lines">A collection of lines ordered.</returns>
        /// <returns name="i">Indexes ordered.</returns>
        /// <returns name="Revers">Line that have to be reversed to have the point continue.</returns>
        /// <search>lines, order</search>
        [MultiReturn(new[] { "Lines", "i", "Revers" })]
        public static IDictionary OrderLinesByProximity(List<Line> lines)
        {
            if (lines.Count == 0)
            {
                return new Dictionary<string, object>
                {
                    {"Lines", lines},
                    {"i", Array.Empty<int>()},
                    {"Revers", Array.Empty<bool>()}
                };
            }

            var convertedLines = lines.Select(l => l.DyToGs());
            var result = LineHelper.OrderByProximity(convertedLines);

            var orderedLines = result.itemsOrdered.Select(i => lines[i]);

            return new Dictionary<string, object>
            {
                {"Lines", orderedLines},
                {"i", result.itemsOrdered},
                {"Revers", result.revers}
            };
        }
    }
}
