using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SampleApp.ViewModels;
using Newtonsoft.Json;

namespace SampleApp.Repos
{
    internal class AzDo : IAzDo
    {
        internal string _organizationUrl = "";
        internal string _project = "";
        internal string _adop = "";

        public AzDo(string adop, string organizationUrl, string project)
        {
            _adop = adop;
            _organizationUrl = organizationUrl;
            _project = project;            
        }

        /// <summary>
        /// fetch full list of available connections
        /// </summary>
        /// <returns></returns>
        public ApiResponses.AzDoGitHubConnections FetchConnections()
        {
            ApiResponses.AzDoGitHubConnections result = new ApiResponses.AzDoGitHubConnections();

            using (var client = new HttpClient())            {
                
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", _adop))));
                                
                HttpResponseMessage response = client.GetAsync($"{_organizationUrl}/{_project}/_apis/githubconnections?api-version=7.1-preview.1").Result;                                
                var json = response.Content.ReadAsStringAsync().Result.ToString();

                result.StatusCode = response.StatusCode;
                result.Success = response.StatusCode == System.Net.HttpStatusCode.NoContent ? true : false;                
                result.value = JsonConvert.DeserializeObject<GitHubConnectionValue>(json);                

                client.Dispose();

                return result;
            }
        }

        /// <summary>
        /// fetch all the connected repos for a given connection id
        /// </summary>
        /// <param name="connectionId">id for connection</param>
        /// <returns></returns>
        public ApiResponses.AzDoGitHubRepos FetchRepos(string connectionId)
        {
            ApiResponses.AzDoGitHubRepos result = new ApiResponses.AzDoGitHubRepos();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", _adop))));
                                
                HttpResponseMessage response = client.GetAsync($"{_organizationUrl}/{_project}/_apis/githubconnections/{connectionId}/repos?api-version=7.1-preview.1").Result;
                var json = response.Content.ReadAsStringAsync().Result.ToString();

                result.StatusCode = response.StatusCode;
                result.Success = response.StatusCode == System.Net.HttpStatusCode.NoContent ? true : false;              
                result.value = JsonConvert.DeserializeObject<GitHubReposValue>(json);

                client.Dispose();

                return result;
            }
        }

        /// <summary>
        /// post update of repos to add or remove connections by repo
        /// </summary>
        /// <param name="connectionId">id of connection</param>
        /// <param name="payload">body of patch document</param>
        /// <returns></returns>
        public ApiResponses.AzDoGitHubRepos PostRepos(string connectionId, string payload)
        {
            ApiResponses.AzDoGitHubRepos result = new ApiResponses.AzDoGitHubRepos();

            using (var client = new HttpClient())
            {                
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", _adop))));
                                
                HttpContent body = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync($"{_organizationUrl}/{_project}/_apis/githubconnections/{connectionId}/repos?api-version=7.1-preview.1", body).Result;
                var json = response.Content.ReadAsStringAsync().Result.ToString();

                result.StatusCode = response.StatusCode;
                result.Success = response.StatusCode == System.Net.HttpStatusCode.NoContent ? true : false;          
                result.value = JsonConvert.DeserializeObject<GitHubReposValue>(json);

                client.Dispose();

                return result;
            }
        }
    }

    interface IAzDo
    {
        ApiResponses.AzDoGitHubConnections FetchConnections();
        ApiResponses.AzDoGitHubRepos FetchRepos(string connectionId);
        ApiResponses.AzDoGitHubRepos PostRepos(string connectionId, string payload);
    }
}
