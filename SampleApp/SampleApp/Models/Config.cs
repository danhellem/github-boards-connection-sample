using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Models
{
    public class Config
    {
        public string AzDoOrgUrl { get; set; }
        public string AzDoProject { get; set; }
        public string AzDoPersonalAccessToken {  get; set; }
        public string GitHubPersonalAccessToken { get; set; }
    }
}
