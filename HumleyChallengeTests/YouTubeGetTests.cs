using HumleyChallengeTests.TestObjects;
using Newtonsoft.Json;
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
    public class YouTubeGetTests
    {
        [Fact]
        public async Task PerformSimpleGetRequestAsync()
        {
            string responseBody = "";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //google api user a devloper key and part section using snippet value             
                    HttpResponseMessage response = await client.GetAsync("https://www.googleapis.com/youtube/v3/search?part=snippet&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
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
            Assert.True(responseBody != "");
        }
        [Fact]
        public async Task PerformSimpleGetRequestWithSearchAsync()
        {
            string responseBody = "";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //google api user a devloper key and part section using snippet value and q search value of surf            
                    HttpResponseMessage response = await client.GetAsync("https://www.googleapis.com/youtube/v3/search?part=snippet&q=surf&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
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
            Assert.True(responseBody != "");
        }
        

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
        public async Task GetDataWithCustomQuery()
        {
            StringBuilder youtubeQuery = new StringBuilder();
            youtubeQuery.Append("https://www.googleapis.com/youtube/v3/search?part=snippet");
            youtubeQuery.Append("&q=surf");
            youtubeQuery.Append("&key=AIzaSyB7MgMx73OCowlgVGIKiXEb94gumnYqh1Q");
            string youtubeResponse = await GetYouTubeData(youtubeQuery.ToString());
            Assert.False(String.IsNullOrEmpty(youtubeResponse));
        }
    }
}
