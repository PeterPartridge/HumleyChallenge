using Business_Layer;
using Data_Layer.classes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ViewLayer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeDataController : ControllerBase
    {
        AppDetails _appDetails;
        public YouTubeDataController(AppDetails appDetails)
        {
            _appDetails = appDetails;
        }

        [HttpGet("{query}")]
        public async Task <ActionResult<YouTubeMasterResponse>> GetListAsync(string query)
        {
            YouTubeQueryMain queryMain = new YouTubeQueryMain(_appDetails);
            return await queryMain.runGetQuery(query);            
        }
    }
}
