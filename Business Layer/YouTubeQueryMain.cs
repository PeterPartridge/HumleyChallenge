using Data_Layer;
using Data_Layer.classes;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class YouTubeQueryMain
    {
        AppDetails _appDetails; 
        public YouTubeQueryMain(AppDetails appDetails)
        {
            _appDetails = appDetails;
        }
        public async Task<YouTubeMasterResponse> runGetQuery(string query)
        {
            YoutubeQuery youtubeQuery = new YoutubeQuery();
            YoutubeResponse youtubeResponse = new YoutubeResponse();
            YoutubeGetRequest getRequest = new YoutubeGetRequest();
            string builtQuery = youtubeQuery.build(_appDetails, query);
            return youtubeResponse.buildResponse(await getRequest.GetYouTubeData(builtQuery));
        }
    }
}
