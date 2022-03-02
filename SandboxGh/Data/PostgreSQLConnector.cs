using System;
using Grasshopper.Kernel;
using SandboxGh.Attributes;
using SandboxCore.Data;

namespace SandboxGh.Data
{
    public class PostgreSQLConnector : SandboxComponent
    {
        public PostgreSQLConnector()
          : base("This component connects to a Postgres database and returns the connection string'", "SQL")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Server", "S", "The server to connect to. The default IP is localhost.", GH_ParamAccess.item, "localhost");
            pManager.AddTextParameter("Port", "P", "The port to connect to. The default port is 5432 for Azure PostgreSQL", GH_ParamAccess.item, "5432");
            pManager.AddTextParameter("Username", "U", "Username to connect with.", GH_ParamAccess.item);
            pManager.AddTextParameter("Password", "PW", "Password to connect with.", GH_ParamAccess.item);
            pManager.AddTextParameter("Database", "D", "Target database to connect to.", GH_ParamAccess.item);

        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Connection Success", "CS", "Notifies a successful connection.", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string server = String.Empty;
            string port = String.Empty;
            string uid = String.Empty;
            string pw = String.Empty;
            string db = String.Empty;
            if (!DA.GetData(0, ref server) | !DA.GetData(1, ref port) | !DA.GetData(2, ref uid) | !DA.GetData(3, ref pw) | !DA.GetData(4, ref db)) return;
        
        
            ///SQLConnector.PostgresConnect(server, port, uid, pw, db);

            DA.SetData(0, "Test output");
        }

        protected override System.Drawing.Bitmap Icon => Resources.PostgreSQLConnectorIcon;

        public override Guid ComponentGuid => new Guid("2DD78996-31C9-44A8-A884-BB4AAC9741B7");
    }
}