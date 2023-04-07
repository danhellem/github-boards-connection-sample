using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    public class GitHubConnectionValue
    {        
        public int count { get; set; }
        public Value[] value { get; set; }        

        public class Value
        {
            public string id { get; set; }
            public string name { get; set; }      
           
            public bool isConnectionValid { get; set; }
        }
    }
}
