using System.Collections.Generic;

namespace Data_Layer.classes
{
    public class YouTubeMasterResponse
    {
        public YouTubeMasterResponse()
        {
            snippits = new List<snippit>();
        }
        public string kind { get; set; }
        public string nextPageToken { get; set; }
        public string prevPageToken { get; set; }
        public List<snippit> snippits { get; set; }
        public bool Error { get; set; }
    }
}
