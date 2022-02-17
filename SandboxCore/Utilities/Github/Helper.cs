using Octokit;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace SandboxCore.Utilities.Github
{
    public static class Helper
    {
        private static GitHubClient _gitHubClient = new GitHubClient(new ProductHeaderValue("MyGit"));
        private static string _assetUrl;
        private static string _releaseTagName;
        public delegate Task<string> DelEvent();

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
                    _releaseTagName = lastRelease.TagName;
                    return _releaseTagName;
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
            var fileName = $@"{desktopDir}\Sandbox_{_releaseTagName}.zip";

            return Task.Run(() =>
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFileAsync(new Uri(_assetUrl), fileName);
                    }
                    return $"Release {_releaseTagName} downloaded on the desktop!";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            });
        }

        public static void SandboxDocumentation() => Process.Start(@"https://mirco-bianchini.gitbook.io/sandbox/");
    }
}
