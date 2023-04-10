using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Repos
{
    public class GitHubRepos : IGitHubRepos
    {
        internal string _ghp = "";
        public GitHubRepos(string ghp)
        {
            _ghp = ghp;
        }

        /// <summary>
        /// fetch list of all GitHub repos the person has access to
        /// </summary>
        /// <returns></returns>
        public IList<string> Fetch()
        {
            return Fetch(string.Empty);
        }

        /// <summary>
        /// fetch list of GitHub repos for a specific organization the person has access to
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public IList<string> Fetch(string organization)
        {
            GitHubClient client = new GitHubClient(new Octokit.ProductHeaderValue("octokit.samples"));           
            client.Credentials = new Credentials(_ghp);

            List<string> list = new List<string>();
            
            // and then fetch the repositories for the current user
            IReadOnlyList<Repository> repos = client.Repository.GetAllForCurrent().Result;

            foreach (var repo in repos)
            {
                if (organization == string.Empty || repo.HtmlUrl.Contains(organization))
                {
                    list.Add(repo.HtmlUrl);
                }                
            }

            return list;
        }
    }

    public interface IGitHubRepos
    {
        IList<string> Fetch();
        IList<string> Fetch(string organization);
    }
}
