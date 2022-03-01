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
            var dynamoRevitDir = @"\Dynamo\Dynamo Revit";
            //var dynamoCoreDir = @"\Dynamo\Dynamo Core";
            return GetDllDirectory(dynamoRevitDir, "SandboxCore.dll");
        }

        /// <summary>
        /// Gets the SandboxRevit.dll directory for Revit.
        /// </summary>
        /// <returns>The directory.</returns>
        public static string RevitDir()
        {
            var revitDir = @"\Autodesk\Revit\Addins";
            return GetDllDirectory(revitDir, "SandboxRevit.dll");
        }

        private static string GetDllDirectory(string specificPath, string dllName)
        {
            var path = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), specificPath);
            var file = Directory.GetFiles(path, dllName, SearchOption.AllDirectories);

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
        /// Gets the ExampleFile.dyn directory.
        /// </summary>
        public static string DynExampleFileDir()
        {
            var path = Path.GetDirectoryName(DynamoDir());
            var parent = Path.GetDirectoryName(path);
            return $@"{parent}\extra\ExampleFile.dyn";
        }

        /// <summary>
        /// Gets the installed product version of Sandbox.
        /// </summary>
        /// <returns>The sandbox version.</returns>
        public static string GetSandboxVersion(string path)
        {
            if (!File.Exists(path)) return $"The Sandbox package wasn't found.\n Check you installed the library correctly.";
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(path);

            return versionInfo.ProductVersion;
        }
    }
}
