﻿using FBTargeting.Algorithms;
using FBTargeting.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeepTargeting.Services
{
    public class FBInterestsQueryService : IQueryService
    {
        public List<Interest> GetKeywordInterests(string queryText)
        {
            List<Interest> foundInterests = new List<Interest>();
            if (queryText != "")
            {
                string url = "https://graph.facebook.com/search?type=adinterest&q=[" + queryText + "]&limit=10000&locale=en_US&access_token=2334601333305211|ixo93bORPMZUVQVdib6xNSHtG-Y";
                string responseString = GetRequest(url);
                responseString = responseString.Remove(0, 8);
                responseString = responseString.Remove(responseString.Length - 1);

                JArray interests = JArray.Parse(responseString);
                foreach (JObject interest in interests)
                {
                    Interest newInterest = new Interest();
                    foreach (KeyValuePair<string, JToken> row in interest)
                    {
                        if (row.Key == "name")
                        {
                            newInterest.Name = row.Value.ToString();
                        }
                        else if (row.Key == "audience_size")
                        {
                            double audienceSize = double.Parse(row.Value.ToString());
                            string humanizedNumber = audienceSize.Nice(5);
                            newInterest.AudienceSize = humanizedNumber;
                        }
                        else if (row.Key == "topic")
                        {
                            newInterest.Category = row.Value.ToString();
                        }
                    }
                    newInterest.GoogleSearchUrl = "https://www.google.com/search?q=" + Regex.Replace(newInterest.Name, @"\s+", "+");
                    foundInterests.Add(newInterest);
                }
            }

            return foundInterests;
        }

        private string GetRequest(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}