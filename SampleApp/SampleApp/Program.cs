using Octokit;
using SampleApp.Helpers;
using SampleApp.Repos;
using SampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp
{
    internal class Program
    {
        // Note: This is sample code only and it should not be using in a production environment without modification and optimization

        static void Main(string[] args)
        {
            ConfigHelper helper = new ConfigHelper();

            // set values from config.json file
            string _ghp = helper.GitHubPersonalAccessToken;   // GitHub personal access token
            string _adop = helper.AzDoPersonalAccessToken;    // Azure DevOps personal acces token
            string _orgurl = helper.AzDoOrgUrl;               // https://dev.azure.com/{organization}
            string _project = helper.AzDoProject;             // Azure DevOps project
            bool isPayload = false;

            helper = null;

            AzDo azdo_client = new AzDo(_adop, _orgurl, _project);
            
            // get all connections from AzDo
            ApiResponses.AzDoGitHubConnections azdo_connections = azdo_client.FetchConnections();

            // getting connection id from first connection in list
            string connectionId = azdo_connections.value.value[0].id;

            // get list of repos for the given connectionId
            ApiResponses.AzDoGitHubRepos azdo_repos = azdo_client.FetchRepos(connectionId);

            // get all the GitHub repos (from github.com) the person has access to
            GitHubRepos gh_repos = new GitHubRepos(_ghp);
            var list = gh_repos.Fetch("{github organization to get repos from}");   //used to filter by a specific organization

            // building a payload to update the connection
            string payload = "{ " +
                " \"gitHubRepositoryUrls\": [ ";
             
            // loop through each repo in list from github
            foreach(var item in list)
            {
                // check to make sure the repo we are trying to add is not already connected
                if (! azdo_repos.value.value.Any(x => x.gitHubRepositoryUrl.Contains(item))) {
                    isPayload = true;
                    payload += "   { \"gitHubRepositoryUrl\": \"" + item.ToString() + "\" }, ";
                }
            }

            payload += " ]," +
                " \"operationType\":\"add\" " +
                "}";

            // if we have any items to add, then continue and attempt to post
            if (isPayload)
            {
                ApiResponses.AzDoGitHubRepos results = azdo_client.PostRepos(connectionId, payload);
            }
            else
            {
                Console.WriteLine("No new repositories to add");
            }

            gh_repos = null;
            list = null;
            azdo_repos = null;
            azdo_connections = null;
            azdo_client = null;
        }
    }
}
