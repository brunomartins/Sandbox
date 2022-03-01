using Octokit;
using System;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using SandboxWpf.View;

namespace SandboxCore.Utilities.Github
{
    public class ReleaseHelper
    {
        private readonly GitHubClient _gitHubClient = new GitHubClient(new ProductHeaderValue("SandboxClient"));
        private string _assetUrl;
        private string _releaseVersion;
        private readonly string _localVersion;

        public ReleaseHelper(string sandboxDirectory)
        {
            GetLastTagRelease();
            _localVersion = Package.GetSandboxVersion(sandboxDirectory);
        }

        /// <summary>
        /// Get the last release from GitHub.
        /// </summary>
        /// <returns>The last release.</returns>
        public void GetLastTagRelease()
        {
            var release = Task.Run(async () => await _gitHubClient.Repository.Release.GetLatest("mottmacdonaldglobal", "Sandbox"));
            try
            {
                _releaseVersion = release.Result.TagName;
                var asset = release.Result.Assets[0];
                _assetUrl = asset.BrowserDownloadUrl;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Download the last release of Sandbox.
        /// </summary>
        /// <returns>The asset from the last release.</returns>
        public void DownloadRelease(object sender, EventArgs e)
        {
            var desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var fileName = $@"{desktopDir}\Sandbox_{_releaseVersion}.zip";

            Task.Run(() =>
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFileAsync(new Uri(_assetUrl), fileName);
                    }
                    MessageBox.Show($"Release {_releaseVersion} downloaded on the desktop!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        /// <summary>
        /// Check if Sandbox is updated looking to the last release.
        /// </summary>
        internal bool IsSandboxUpdated
        {
            get
            {
                var releaseVersion = _releaseVersion;
                if (releaseVersion.Contains("alpha"))
                {
                    releaseVersion = Regex.Replace(releaseVersion, "[a-zA-Z -]", "");
                }
                return _localVersion == releaseVersion;
            }
        }

        /// <summary>
        /// Return the current status of the local and release version.
        /// </summary>
        private string StatusOfTheVersion()
        {
            return (IsSandboxUpdated)
                ? $"Your are update to the version: {_localVersion}"
                : $"Your local version: {_localVersion}\n" +
                  $"Last release: {_releaseVersion}\n" +
                  "Alpha version meaning new features in development.";
        }

        /// <summary>
        /// Fires the splash banner with the version information.
        /// </summary>
        public void CheckForUpdates(object sender, EventArgs e)
        {
            var splashAbout = new SplashAbout(StatusOfTheVersion());
            splashAbout.Show();
        }

        /// <summary>
        /// Opens the documentation webpage of Sandbox.
        /// </summary>
        public static void SandboxDocumentation(object sender, EventArgs e) => Process.Start(@"https://mirco-bianchini.gitbook.io/sandbox/");
    }
}
