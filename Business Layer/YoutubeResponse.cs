using Data_Layer.classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_Layer
{
   public class YoutubeResponse
    {
        /// <summary>
        /// Youtube returns a string response this will take the repsonse and build a repsonse class. 
        /// </summary>
        /// <param name="responseBody"></param>
        /// <returns></returns>
        public YouTubeMasterResponse buildResponse(string responseBody)
        {
            YouTubeMasterResponse result = new YouTubeMasterResponse();
            try
            {
                JObject youTubeObject = JObject.Parse(responseBody);
                result = youTubeObject.ToObject<YouTubeMasterResponse>();
                IList<JToken> ListResults = youTubeObject["items"].Children().ToList();
                int i = 0;
                foreach (var item in ListResults)
                {
                    result.snippits.Add(item["snippet"].ToObject<snippit>());
                    result.snippits[i].url = item["snippet"]["thumbnails"]["default"]["url"].ToObject<string>();
                    if (item["id"]["videoId"] != null)
                    {
                        result.snippits[i].videoId = item["id"]["videoId"].ToObject<string>();
                    }
                    //stop chennelid auto population just incase issue arises.
                    if (item["id"]["channelId"] != null)
                    {
                        result.snippits[i].channelId = item["id"]["channelId"].ToObject<string>();
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                StringBuilder errorBuilder = new StringBuilder();
                errorBuilder.AppendLine("We have had an error creating the YoutubeObject");
                errorBuilder.AppendLine(ex.Message);
                throw new Exception($"{errorBuilder.ToString()}");
            }
            return result;
        }
    }
}
