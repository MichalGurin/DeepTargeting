using System;
using System.Collections;
using System.Collections.Generic;

namespace DeepTargeting.Models
{
    public class QueryViewModel : ICloneable
    {
        public Query CreatedQuery { get; set; } = new Query();

        public List<Interest> FoundInterests { get; set; } = new List<Interest>();

        public List<string> PreviousQueries { get; set; } = new List<string>();

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
