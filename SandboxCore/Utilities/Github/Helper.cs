using System.Diagnostics;
using System.Threading.Tasks;
using Octokit;

namespace SandboxCore.Utilities.Github
{
    public static class Helper
    {
        public static async Task<string> GetReleaseVersion()
        {
            var client = new GitHubClient(new ProductHeaderValue("MyGit"));
            var releases = await client.Repository.Release.GetAll("mottmacdonaldglobal", "Sandbox");

            return releases[0].TagName;
        }

        public static void SandboxDocumentation() => Process.Start(@"https://mirco-bianchini.gitbook.io/sandbox/");
    }
}
