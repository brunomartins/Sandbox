using System;
using System.Diagnostics;
using System.IO;

namespace SandboxCore.Utilities
{
    public static class Package
    {
        /// <summary>
        /// Gets the SandboxCore.dll directory for Dynamo.
        /// </summary>
        /// <returns>The directory.</returns>
        public static string DynamoDir()
        {
            var dynamoDir = @"\Dynamo\Dynamo Core";
            var path = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), dynamoDir);
            var file = Directory.GetFiles(path, "SandboxCore.dll", SearchOption.AllDirectories);

            return file.Length == 0 ? string.Empty : file[0];
        }

        /// <summary>
        /// Gets the SandboxCore.dll directory for Grasshopper.
        /// </summary>
        /// <returns>The directory.</returns>
        public static string GrasshopperDir()
        {
            var fileDir = @"\Grasshopper\Libraries\SandboxGh\SandboxCore.dll";
            return string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileDir);
        }

        /// <summary>
        /// Gets the installed product version of Sandbox.
        /// </summary>
        /// <returns>The sandbox version.</returns>
        public static string GetSandboxVersion(string path)
        {
            if (!File.Exists(path)) return string.Empty;
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(path);

            return versionInfo.ProductVersion;
        }
    }
}
