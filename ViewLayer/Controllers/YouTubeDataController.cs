using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ViewLayer.Controllers
{
    [Route("api/[controller]")]
    public class YouTubeDataController : Controller
    {
        [HttpGet("[action]")]
        public void getList()
        {
            
        }
    }
}
