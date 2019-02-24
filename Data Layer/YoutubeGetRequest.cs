using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
   public class YoutubeGetRequest
    {
        public async Task<string> GetYouTubeData(string youtubeQuery)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //google api user a devloper key and part section using snippet value and q search value of surf            
                    HttpResponseMessage response = await client.GetAsync(youtubeQuery);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                    // deserialise Json headers into Master response.

                }
                catch (UnauthorizedAccessException ex)
                {
                    StringBuilder error = new StringBuilder();
                    error.AppendLine("Error unathorised access Youtube");
                    error.AppendLine(ex.StackTrace);
                    error.AppendLine(ex.Message);
                    throw new Exception(error.ToString());
                }
                catch (HttpRequestException ex)
                {
                    StringBuilder error = new StringBuilder();
                   
                    error.AppendLine("Http error occured getting dsata from Youtube");
                    error.AppendLine(ex.StackTrace);
                    error.AppendLine(ex.Message);
                    throw new Exception(error.ToString());
                }
                catch (Exception ex)
                {
                    StringBuilder error = new StringBuilder();
                    error.AppendLine("Error occured getting Data from Youtube");
                    error.AppendLine(ex.StackTrace);
                    error.AppendLine(ex.Message);
                    throw new Exception(error.ToString());
                }

            }
        }
    }
}
