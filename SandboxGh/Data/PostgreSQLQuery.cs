using System;
using Grasshopper.Kernel;
using SandboxGh.Attributes;
using SandboxCore.Data;

namespace SandboxGh.Data
{
    public class PostgreSQLQuery : SandboxComponent
    {
        public PostgreSQLQuery()
          : base("This component executes a query on a database.'", "SQL")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Connection String", "CS", "The Connection String from the PostgreSQLConnector component.", GH_ParamAccess.item);
            pManager.AddTextParameter("Query", "Q", "The Query to execute.", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Result", "R", "The query result.", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string connectionString = String.Empty;
            string query = String.Empty;
            if (!DA.GetData(0, ref connectionString) | !DA.GetData(1, ref query)) return;
 
           /// DA.SetData(0, state);
        }

        protected override System.Drawing.Bitmap Icon => Resources.PostgreSQLQueryIcon;

        public override Guid ComponentGuid => new Guid("2DD78996-31C9-44A8-A884-BB4AAC9741B4");
    }
}