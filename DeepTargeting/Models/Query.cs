using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FBTargeting.Models
{
    public class Query
    {
        [Required]
        public string QueryText { get; set; } = "";

        public string Language { get; set; } = "en-us";
    }
}
