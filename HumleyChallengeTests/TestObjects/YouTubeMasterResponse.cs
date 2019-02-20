using System.Collections.Generic;

namespace HumleyChallengeTests.TestObjects
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

    }
}
