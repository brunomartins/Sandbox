using System;
using Octokit;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace SandboxCore.Utilities.Github
{
    public static class Helper
    {
        public delegate Task<string> DelEvent();

        /// <summary>
        /// Get the last release from GitHub.
        /// </summary>
        /// <returns>The last release.</returns>
        public static Task<string> GetLastTagRelease()
        {
            return Task.Run(async () =>
            {
                var client = new GitHubClient(new ProductHeaderValue("MyGit"));
                var lastRelease = await client.Repository.Release.GetLatest("mottmacdonaldglobal", "Sandbox");
                return lastRelease.TagName;
            });
        }

        /// <summary>
        /// Gets the asset into the last release.
        /// </summary>
        /// <returns>The asset from the last release.</returns>
        public static Task<string> GetLastAsset()
        {
            return Task.Run(async () =>
            {
                var gitClient = new GitHubClient(new ProductHeaderValue("MyGit"));
                var lastRelease = await gitClient.Repository.Release.GetLatest("mottmacdonaldglobal", "Sandbox");
                var asset = lastRelease.Assets[0];
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri(asset.BrowserDownloadUrl),
                        @"C: \Users\BIA97506\OneDrive - Mott MacDonald\Desktop\SandboxRelease.zip");
                }
                return asset.Name;
            });
        }

        public static void SandboxDocumentation() => Process.Start(@"https://mirco-bianchini.gitbook.io/sandbox/");
    }
}
