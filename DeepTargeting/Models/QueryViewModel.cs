using System.Collections.Generic;
using System.Collections;
using DeepTargeting.Models;
using System;

namespace DeepTargeting.Models
{
    public class QueryViewModel : ICloneable
    {
        public Query CreatedQuery { get; set; } = new Query();

        public List<Interest> FoundInterests { get; set; } = new List<Interest>();

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
