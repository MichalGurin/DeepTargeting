using System;
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

public class QueryPageModel : PageModel
{
    [BindProperty]
    public Query CreatedQuery { get; set; }

    public List<Interest> FoundInterests { get; set; } = new List<Interest>();

    public void OnGet()
    {
        CreatedQuery = new Query();
    }

    public void OnPost()
    {
        if (CreatedQuery.QueryText != "")
        {
            string url = "https://graph.facebook.com/search?type=adinterest&q=[" + CreatedQuery.QueryText + "]&limit=10000&locale=en_US&access_token=2334601333305211|ixo93bORPMZUVQVdib6xNSHtG-Y";
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
                newInterest.GoogleSearchUrl = "https://www.google.com/search?q=" +  Regex.Replace(newInterest.Name, @"\s+", "+");
                FoundInterests.Add(newInterest);
            }
            RedirectToPage("/DeepTargeting/QueryPage");
        }
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