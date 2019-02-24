using HumleyChallengeTests.TestObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HumleyChallengeTests
{
    public class YoutubeResponseTests
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
                    throw new Exception($"Error unautherised acccess. {ex.Message}");
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }

        [Fact]
        public async Task SearchAndBuildObjectAsync()
        {
            YouTubeMasterResponse result = new YouTubeMasterResponse();

            string responseBody = await GetYouTubeData("https://www.googleapis.com/youtube/v3/search?part=snippet&q=surf&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");

            try
            {
                JObject youTubeObject = JObject.Parse(responseBody);
                result = youTubeObject.ToObject<YouTubeMasterResponse>();
            }
            catch (Exception ex)
            {

                throw new Exception($"Error {ex.Message}");
            }

            Assert.True(result != new YouTubeMasterResponse());

        }
        [Fact]
        public async Task UseNextTokenAsync()
        {
            YouTubeMasterResponse result = new YouTubeMasterResponse();
            YouTubeMasterResponse resultNextPage = new YouTubeMasterResponse();
            string responseBody = await GetYouTubeData("https://www.googleapis.com/youtube/v3/search?part=snippet&q=surf&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
            StringBuilder youtubeQuery = new StringBuilder();
            try
            {
                JObject youTubeObject = JObject.Parse(responseBody);
                result = youTubeObject.ToObject<YouTubeMasterResponse>();
                // go to the next page
                youtubeQuery.Append("https://www.googleapis.com/youtube/v3/search?part=snippet");
                youtubeQuery.Append($"&pageToken={result.nextPageToken}");
                youtubeQuery.Append("&q=surf");
                youtubeQuery.Append($"&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
                responseBody = await GetYouTubeData(youtubeQuery.ToString());
                JObject youTubeObjectNextPage = JObject.Parse(responseBody);
                resultNextPage = youTubeObjectNextPage.ToObject<YouTubeMasterResponse>();
            }
            catch (Exception ex)
            {

                throw new Exception($"Error {ex.Message}");
            }
            Assert.True(result.nextPageToken != resultNextPage.nextPageToken);
            Assert.True(!string.IsNullOrEmpty(resultNextPage.prevPageToken));
        }
        [Fact]
        public async Task UsePrviouseTokenAsync()
        {
            YouTubeMasterResponse result = new YouTubeMasterResponse();
            YouTubeMasterResponse resultPrevPage = new YouTubeMasterResponse();
            StringBuilder youtubeQuery = new StringBuilder();
            try
            {
                string responseBody = await GetYouTubeData("https://www.googleapis.com/youtube/v3/search?part=snippet&q=surf&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
                JObject youTubeObject = JObject.Parse(responseBody);
                //go to next page
                result = youTubeObject.ToObject<YouTubeMasterResponse>();
                youtubeQuery.Append("https://www.googleapis.com/youtube/v3/search?part=snippet");
                youtubeQuery.Append($"&pageToken={result.nextPageToken}");
                youtubeQuery.Append("&q=surf");
                youtubeQuery.Append($"&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
                responseBody = await GetYouTubeData(youtubeQuery.ToString());
                JObject youTubeObjectNextPage = JObject.Parse(responseBody);
                YouTubeMasterResponse resultNextPage = youTubeObjectNextPage.ToObject<YouTubeMasterResponse>();
                // go back to orginal page.
                youtubeQuery = new StringBuilder();
                youtubeQuery.Append("https://www.googleapis.com/youtube/v3/search?part=snippet");
                youtubeQuery.Append($"&pageToken={resultNextPage.prevPageToken}");
                youtubeQuery.Append("&q=surf");
                youtubeQuery.Append($"&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
                responseBody = await GetYouTubeData(youtubeQuery.ToString());
                JObject youTubeObjectPrevPage = JObject.Parse(responseBody);
                resultPrevPage = youTubeObjectPrevPage.ToObject<YouTubeMasterResponse>();

            }
            catch (Exception ex)
            {

                throw new Exception($"Error {ex.Message}");
            }
            //page tokens should be the same.
            Assert.True(result.nextPageToken == resultPrevPage.nextPageToken);
        }
        [Fact]
        public async Task GetSnippetAsync()
        {
            YouTubeMasterResponse result = new YouTubeMasterResponse();
            try
            {

                string responseBody = await GetYouTubeData("https://www.googleapis.com/youtube/v3/search?part=snippet&q=surf&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
                // deserialise Json headers into Master response.
                JObject youTubeObject = JObject.Parse(responseBody);
                IList<JToken> ListResults = youTubeObject["items"].Children().ToList();
                foreach (var item in ListResults)
                {
                    result.snippits.Add(item["snippet"].ToObject<snippit>());
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }

            Assert.True(result.snippits.Count > 1);
        }
        [Fact]
        public async Task GetThumbnailAsync()
        {
            List<string> result = new List<string>();
            try
            {
                string responseBody = await GetYouTubeData("https://www.googleapis.com/youtube/v3/search?part=snippet&q=surf&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
                // deserialise Json headers into Master response.
                JObject youTubeObject = JObject.Parse(responseBody);
                IList<JToken> ListResults = youTubeObject["items"].Children().ToList();
                foreach (var item in ListResults)
                {
                    //get default image and will use Angular bootstrap to size the image.
                    result.Add(item["snippet"]["thumbnails"]["default"]["url"].ToObject<string>());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            Assert.True(result.Count == 5);
        }

        public YouTubeMasterResponse buildYoutubeResponse(string responseBody)
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

        [Fact]
        public async Task GetYoutubeObject()
        {
            string responseBody = await GetYouTubeData("https://www.googleapis.com/youtube/v3/search?part=snippet&q=surf&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
            // deserialise Json headers into Master response.
            YouTubeMasterResponse result = buildYoutubeResponse(responseBody);
            Assert.True(result.snippits.Count == 5);
            Assert.True(string.IsNullOrEmpty(result.error));
        }
        [Fact]
        public async Task GetYoutubeVideoId()
        {
            string responseBody = await GetYouTubeData("https://www.googleapis.com/youtube/v3/search?part=snippet&q=surf&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
            // deserialise Json headers into Master response.
            YouTubeMasterResponse result = buildYoutubeResponse(responseBody);
            // some youtube channeles do not have a video ID, but all should have a channel ID.
            //ChannelID has also populated without a cast
            Assert.True(!string.IsNullOrEmpty(result.snippits[4].videoId));           
        }
    }
}
