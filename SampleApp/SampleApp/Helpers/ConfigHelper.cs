using Newtonsoft.Json;
using SampleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Helpers
{  
    public class ConfigHelper
    {
        private string _AzDoOrgUrl = "";
        private string _AzDoProject = "";
        private string _AzDoPersonalAccessToken = "";
        private string _GitHubPersonalAccessToken = "";

        public ConfigHelper()
        {
            Init();
        }


        /// <summary>
        /// gets and sets the config values from the config.json file
        /// </summary>
        private void Init()
        {

            string jsonFilePath = "";

#if DEBUG
            jsonFilePath = @"config.debug.json";
#else
            jsonFilePath = @"config.json";
#endif

            try
            {
                Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(jsonFilePath));

                this._AzDoOrgUrl = config.AzDoOrgUrl;
                this._AzDoProject = config.AzDoProject;
                this._AzDoPersonalAccessToken = config.AzDoPersonalAccessToken;
                this._GitHubPersonalAccessToken = config.GitHubPersonalAccessToken;
            }
            catch (Exception)
            {
                this._AzDoOrgUrl = string.Empty;
                this._AzDoProject = string.Empty;
                this._AzDoPersonalAccessToken = string.Empty;
                this._GitHubPersonalAccessToken = string.Empty;
            }
        }

        public string AzDoOrgUrl
        {
            get { return _AzDoOrgUrl; }
        }

        public string AzDoProject
        {
            get { return _AzDoProject; }
        }

        public string AzDoPersonalAccessToken
        {
            get { return _AzDoPersonalAccessToken; }
        }

        public string GitHubPersonalAccessToken
        {
            get { return _GitHubPersonalAccessToken; }
        }


    }

}
