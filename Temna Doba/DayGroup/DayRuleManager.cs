using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temna_Doba.RuleGroup;

namespace Temna_Doba.DayGroup
{
    public class DayRuleManager
    {
        DayRuleRepository dayRuleRepository;
        RuleRepository ruleRepository;
        

        public DayRuleManager(DayRuleRepository dayRuleRepository, RuleRepository ruleRepository)
        {
            this.dayRuleRepository = dayRuleRepository;
            this.ruleRepository = ruleRepository;

        }

        public List<BrokenDayRuleOutput> GetBrokenDayRules()
        {
            List<BrokenDayRuleOutput> brokenDayRuleOutputs = new List<BrokenDayRuleOutput>();
            Dictionary<int, DayRule> dayRuleDictionary = dayRuleRepository.GetDayRuleDictionary();
            foreach(KeyValuePair<int, DayRule> dayRuleKeyValue in dayRuleDictionary)
            {
                BrokenDayRuleOutput brokenDayRuleOutput = new BrokenDayRuleOutput();
                brokenDayRuleOutput.Day = dayRuleKeyValue.Key;
                brokenDayRuleOutput.CitizenBrokenRules = new List<(string citizenName, string brokenRule)>();
                //přidat broken pravidla, pracovat s rulerepository, dayrulerepository, získat identifikátor, přidat do rulerepository metodu get dayrule by identifikátor
                brokenDayRuleOutputs.Add(brokenDayRuleOutput);
            }
            return brokenDayRuleOutputs;
        }
    }
}
