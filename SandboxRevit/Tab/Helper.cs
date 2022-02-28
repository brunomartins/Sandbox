using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Windows;

namespace SandboxRevit.Tab
{
    class Helper
    {
        public static RibbonToolTip ButtonToolTip(string resourceName, string assemblyPath, string content,
            string expandedContent)
        {
            var tempPath = Path.Combine(Path.GetTempPath(), resourceName);

            using (Stream stream = Assembly
                       .GetExecutingAssembly()
                       .GetManifestResourceStream(assemblyPath))
            {
                var buffer = new byte[stream.Length];

                stream.Read(buffer, 0, buffer.Length);

                using (FileStream fs = new FileStream(tempPath,
                           FileMode.Create,
                           FileAccess.Write))
                {
                    fs.Write(buffer, 0, buffer.Length);
                }
            }

            RibbonToolTip toolTip = new RibbonToolTip()
            {
                Content = content,
                ExpandedContent = expandedContent,
                ExpandedVideo = new Uri(Path.Combine(Path.GetTempPath(), resourceName)),
                IsHelpEnabled = true,
                IsProgressive = true
            };

            return toolTip;
        }
    }
}
