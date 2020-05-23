using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepTargeting.Models
{
    public class Interest
    {
        public string Name { get; set; }
        public string AudienceSize { get; set; }
        public string Category { get; set; }
        public string GoogleSearchUrl { get; set; }

        public Interest()
        {

        }

        public Interest(string name, string audienceSize, string category, string googleSearchUrl)
        {
            Name = name;
            AudienceSize = audienceSize;
            Category = category;
            GoogleSearchUrl = googleSearchUrl;
        }
    }
}
