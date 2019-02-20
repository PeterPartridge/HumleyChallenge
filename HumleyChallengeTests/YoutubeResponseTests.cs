using HumleyChallengeTests.TestObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HumleyChallengeTests
{
    public class YoutubeResponseTests
    {

        public async Task<string> getData()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //google api user a devloper key and part section using snippet value and q search value of surf            
                    HttpResponseMessage response = await client.GetAsync("https://www.googleapis.com/youtube/v3/search?part=snippet&q=surf&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
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
                    throw new Exception();
                }
            }
        }

        [Fact]
        public async Task SearchAndBuildObjectAsync()
        {
            YouTubeMasterResponse result = new YouTubeMasterResponse();

            string responseBody = await getData();

            try
            {
                JObject youTubeObject = JObject.Parse(responseBody);
                result = youTubeObject.ToObject<YouTubeMasterResponse>();
            }
            catch (Exception)
            {

                throw;
            }

            Assert.True(result != new YouTubeMasterResponse());

        }
        [Fact]
        public async Task GetSnippetAsync()
        {
            YouTubeMasterResponse result = new YouTubeMasterResponse();
            try
            {

                string responseBody = await getData();
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
        public async Task GetSnippetAndURLAsync()
        {
            List<thumbnail> result = new List<thumbnail>();       
                try
                {               
                    string responseBody = await getData();
                    // deserialise Json headers into Master response.
                    JObject youTubeObject = JObject.Parse(responseBody);
                    IList<JToken> ListResults = youTubeObject["items"].Children().ToList();
                    foreach (var item in ListResults)
                    {
                        result.Add(item["snippet"]["thumbnails"].ToObject<thumbnail>());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            Assert.True(result.Count > 1);
        }
    }
}
