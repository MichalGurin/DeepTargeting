using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeepTargeting.Models
{
    public class Query
    {
        [Required]
        public string QueryText { get; set; } = "";

        public string Language { get; set; } = "en-us";

        public Query()
        {

        }

        public Query(string queryText, string language)
        {
            this.QueryText = queryText;
            this.Language = language;
        }
    }
}
