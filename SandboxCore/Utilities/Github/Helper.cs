using Octokit;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace SandboxCore.Utilities.Github
{
    public static class Helper
    {
        private static readonly GitHubClient _gitHubClient = new GitHubClient(new ProductHeaderValue("SandboxClient"));
        private static string _assetUrl;
        private static string _releaseVersion;
        private static string _localVersion;
        public delegate Task<string> DelEvent();
        private static string _task;

        /// <summary>
        /// Get the last release from GitHub.
        /// </summary>
        /// <returns>The last release.</returns>
        public static Task<string> GetLastTagRelease()
        {
            return Task.Run(async () =>
            {
                try
                {
                    var lastRelease = await _gitHubClient.Repository.Release.GetLatest("mottmacdonaldglobal", "Sandbox");
                    var asset = lastRelease.Assets[0];
                    _assetUrl = asset.BrowserDownloadUrl;
                    _releaseVersion = lastRelease.TagName;
                    return _releaseVersion;
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            });
        }

        /// <summary>
        /// Gets the asset into the last release.
        /// </summary>
        /// <returns>The asset from the last release.</returns>
        public static Task<string> GetLastAsset()
        {
            var desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var fileName = $@"{desktopDir}\Sandbox_{_releaseVersion}.zip";

            return Task.Run(() =>
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFileAsync(new Uri(_assetUrl), fileName);
                    }
                    return $"Release {_releaseVersion} downloaded on the desktop!";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            });
        }

        /// <summary>
        /// The directory to the Sandbox documentation.
        /// </summary>
        public static void SandboxDocumentation() => Process.Start(@"https://mirco-bianchini.gitbook.io/sandbox/");
    }
}
