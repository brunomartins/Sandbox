using Grasshopper.Kernel;
using Rhino.Geometry;
using SandboxCore.Geometry;
using SandboxGh.Attributes;
using SandboxGh.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SandboxGh.GeometryTools
{
    public class OrderLinesByProximity : SandboxComponent
    {
        public OrderLinesByProximity()
          : base("Orders lines by proximity between each others.", "Geometry")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddLineParameter("Lines", "L", "Collection of lines to order", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddLineParameter("Ordered lines", "OL", "Ordered lines", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Ordered indexes", "i", "Ordered indexes", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Revers", "R", "Line that have to be reversed to have the point continue.",
                GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Line> lines = new List<Line>();
            if (!DA.GetDataList(0, lines)) return;

            var convertedLines = lines.Select(l => l.RhToGs());
            var result = LineHelper.OrderByProximity(convertedLines);

            var orderedLines = result.itemsOrdered.Select(i => lines[i]);

            DA.SetDataList(0, orderedLines);
            DA.SetDataList(1, result.itemsOrdered);
            DA.SetDataList(2, result.revers);
        }

        protected override System.Drawing.Bitmap Icon => null;

        public override Guid ComponentGuid => new Guid("F1273AFE-B3B6-4C96-8BB4-C297373EAFFA");
    }
}