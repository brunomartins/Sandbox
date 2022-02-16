using Octokit;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SandboxCore.Utilities.Github
{
    public static class Helper
    {
        public delegate Task<string> DelEvent();

        public static Task<string> GetReleaseVersion()
        {
            return Task.Run(async () =>
            {
                var client = new GitHubClient(new ProductHeaderValue("MyGit"));
                var releases = await client.Repository.Release.GetAll("mottmacdonaldglobal", "Sandbox");
                return releases[0].TagName;
            });
        }

        public static void SandboxDocumentation() => Process.Start(@"https://mirco-bianchini.gitbook.io/sandbox/");
    }
}
