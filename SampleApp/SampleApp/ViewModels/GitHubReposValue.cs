using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    public class GitHubReposValue
    {        
        public int count { get; set; }
        public Value[] value { get; set; }       

        public class Value
        {
            public string gitHubRepositoryUrl { get; set; }
            public string errorMessage { get; set; }
        }
    }
}
