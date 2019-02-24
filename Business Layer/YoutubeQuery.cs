using Data_Layer.classes;
using System;
using System.Text;

namespace Business_Layer
{
    public class YoutubeQuery
    {
        /// <summary>
        /// This will build the query to get info from youtube.
        /// </summary>
        /// <param name="appDetails"></param>
        /// <param name="query"></param>
        /// <returns></returns>
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
