using Business_Layer;
using Data_Layer.classes;
using Microsoft.AspNetCore.Mvc;

namespace ViewLayer.Controllers
{
    [Route("api/[controller]")]
    public class YouTubeDataController : Controller
    {
        //AppDetails _appDetails;
        //public YouTubeDataController(AppDetails appDetails)
        //{
        //    _appDetails = appDetails;
        //}

        [HttpGet("[action]")]
        public void getList([FromBody]string query)
        {
            YoutubeQuery youtubeQuery = new YoutubeQuery();
           // string getRequest = youtubeQuery.build(_appDetails, query);

        }
    }
}
