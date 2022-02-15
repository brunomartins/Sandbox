using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandboxCore.Utilities
{
    public static class Package
    {
        public static string GetSandboxVersion()
        {
            var fileDir = @"\Grasshopper\Libraries\SandboxGh\SandboxCore.dll";
            var path = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileDir);
            var version = string.Empty;

            if (!File.Exists(path)) return version;
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(path);
            version = versionInfo.FileVersion;

            return version;
        }
    }
}
