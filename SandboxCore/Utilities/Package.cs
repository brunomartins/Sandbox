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
            // ToDo: we are searching only for Revit, but it could be expanded also for Core.
            var dynamoCoreDir = @"\Dynamo\Dynamo Core";
            var dynamoRevitDir = @"\Dynamo\Dynamo Revit";
            var path = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), dynamoCoreDir);
            var file = Directory.GetFiles(path, "SandboxCore.dll", SearchOption.AllDirectories);

            return file.Length == 0 ? string.Empty : file[0];
        }

        /// <summary>
        /// Gets the SandboxGh directory.
        /// </summary>
        /// <returns>The directory.</returns>
        public static string SandboxGhDir()
        {
            var fileDir = @"\Grasshopper\Libraries\SandboxGh\";
            return string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileDir);
        }

        /// <summary>
        /// Gets the SandboxCore.dll directory.
        /// </summary>
        public static string SandboxGhAssemblyDir => $"{SandboxGhDir()}SandboxCore.dll";

        /// <summary>
        /// Gets the ExampleFile.gh directory.
        /// </summary>
        public static string GhExampleFileDir => $"{SandboxGhDir()}ExampleFile.gh";

        /// <summary>
        /// Gets the installed product version of Sandbox.
        /// </summary>
        /// <returns>The sandbox version.</returns>
        public static string GetSandboxVersion(string path)
        {
            if (!File.Exists(path)) return $"The Sandbox package wasn't found.\n Checks you installed the library correctly.";
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(path);

            return versionInfo.ProductVersion;
        }
    }
}
