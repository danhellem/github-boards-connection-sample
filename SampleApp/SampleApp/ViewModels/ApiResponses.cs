using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{   
    public class ApiResponses
    {
        public class AzDoGitHubConnections : IApiResponse
        {
            public bool Success { get; set; } = false;
            public HttpStatusCode StatusCode { get; set; }        
            public GitHubConnectionValue value { get; set; }
        }

        public class AzDoGitHubRepos : IApiResponse
        {
            public bool Success { get; set; } = false;
            public HttpStatusCode StatusCode { get; set; }       
            public GitHubReposValue value { get; set; }
        }

        public interface IApiResponse
        {
            bool Success { get; set; }
            HttpStatusCode StatusCode { get; set; }     
        }
    }
}
