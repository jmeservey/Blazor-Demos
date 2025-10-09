using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITProjectTrackerClassLibrary
{
    public class AgentModel
    {
        [JsonProperty("id")]
        public object Id { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("location_id")]
        public object LocationId { get; set; }
        [JsonProperty("job_title")]
        public string JobTitle { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}
