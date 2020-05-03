using System.Collections.Generic;
using System.Collections;
using FBTargeting.Models;


namespace DeepTargeting.Models
{
    public class QueryViewModel
    {
        public Query CreatedQuery { get; set; } = new Query();

        public List<Interest> FoundInterests { get; set; } = new List<Interest>();
    }
}
