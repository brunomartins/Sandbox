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
        public static string DynamoDir()
        {
            var dynamoDir = @"\Dynamo\Dynamo Core";
            var path = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), dynamoDir);
            var file = Directory.GetFiles(path, "SandboxCore.dll", SearchOption.AllDirectories);
       
            return file.Length == 0 ? string.Empty : file[0];
        }

        public static string GrasshopperDir()
        {
            var fileDir = @"\Grasshopper\Libraries\SandboxGh\SandboxCore.dll";
            return string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileDir);
        }

        public static string GetSandboxVersion(string path)
        {
            if (!File.Exists(path)) return string.Empty;
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(path);

            return versionInfo.FileVersion;
        }
    }
}
