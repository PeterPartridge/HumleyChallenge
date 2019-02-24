using Data_Layer.classes;
using System;
using System.Text;

namespace Business_Layer
{
    public class YoutubeQuery
    {
        public string build(AppDetails appDetails, string query)
        {
            StringBuilder youtubeQuery = new StringBuilder();
            youtubeQuery.Append("https://www.googleapis.com/youtube/v3/search?part=snippet&");
            youtubeQuery.Append($"{query}");
            youtubeQuery.Append($"&key={appDetails.YoutubeKey}");
            return youtubeQuery.ToString();
        }
    }
}
