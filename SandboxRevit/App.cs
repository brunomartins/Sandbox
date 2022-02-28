using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;

namespace SandboxRevit
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            throw new NotImplementedException();
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            throw new NotImplementedException();
        }
    }
}
