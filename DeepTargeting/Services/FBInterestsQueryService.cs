using FBTargeting.Algorithms;
using DeepTargeting.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeepTargeting.Services
{
    public class FBInterestsQueryService : IQueryService
    {
        public async Task<List<Interest>> GetKeywordInterests(Query query)
        {
            List<Interest> foundInterests = new List<Interest>();
            if (query.QueryText != "")
            {
                string url = "https://graph.facebook.com/search?type=adinterest&q=[" + query.QueryText + "]&limit=10&locale=" + query.Language + "&access_token=2334601333305211|ixo93bORPMZUVQVdib6xNSHtG-Y";
                string responseString = await GetRequestAsync(url);
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
                            string humanizedNumber = audienceSize.Nice(4);
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

        private async Task<string> GetRequestAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
