using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temna_Doba.DayGroup
{
    public class BrokenDayRuleOutput
    {
        public int Day { get; set; }
        public List<(string citizenName, string brokenRule)> CitizenBrokenRules { get; set; }
    }
}
