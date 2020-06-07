using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DeepTargeting.Models
{
    public class Query
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string QueryText { get; set; } = "";

        public string Language { get; set; } = "en-us";

        public string UserId { get; set; }

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
