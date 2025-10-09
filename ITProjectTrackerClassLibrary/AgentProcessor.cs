using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITProjectTrackerClassLibrary
{
    public class AgentProcessor
    {
        public static async Task<AgentModel> LoadAgentInformation()
        {
            string url = "https://smchelpdesk.freshservice.com/api/v2/agents/21001449415";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    AgentResultModel result = await response.Content.ReadAsAsync<AgentResultModel>();

                    return result.Agent;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task<List<AgentModel>> LoadAgentsInformation()
        {
            string url = "https://smchelpdesk.freshservice.com/api/v2/agents";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    //return JsonConvert.DeserializeObject<AgentResultModel>(stringData);

                    AgentResultModel result = await response.Content.ReadAsAsync<AgentResultModel>();

                    return result.Agents;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
