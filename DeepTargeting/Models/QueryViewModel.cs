using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FBTargeting.Algorithms;
using FBTargeting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;


namespace DeepTargeting.Models
{
    public class QueryViewModel
    {
        public Query CreatedQuery { get; set; }

        public List<Interest> FoundInterests { get; set; } = new List<Interest>();
    }
}
