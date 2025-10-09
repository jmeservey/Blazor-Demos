using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary
{
    public class AgentResultModel
    {
        public AgentModel Agent { get; set; }
        public List<AgentModel> Agents { get; set; }
    }
}
