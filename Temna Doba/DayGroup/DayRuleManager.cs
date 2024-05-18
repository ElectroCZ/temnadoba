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
                
                DayRule dayRule = dayRuleKeyValue.Value;
                List<Rule> activeRules = new List<Rule>();

                foreach (string identificator in dayRule.RuleIdentificatorList)
                {
                    Rule rule = ruleRepository.GetRuleById(identificator);
                    if (rule != null)
                    {
                        activeRules.Add(rule);
                    }
                }

                foreach (Citizen citizen in dayRule.CitizenList)
                {
                    foreach(Rule rule in activeRules)
                    {
                        bool isRuleBroken = HasCitizenBrokenRule(citizen, rule);
                        if (isRuleBroken)
                        {
                            brokenDayRuleOutput.CitizenBrokenRules.Add((citizen.Name, rule.Identificator));
                        }
                    }                    
                }
                brokenDayRuleOutputs.Add(brokenDayRuleOutput);
            }
            return brokenDayRuleOutputs;
        }

        private bool HasCitizenBrokenRule(Citizen citizen, Rule rule)
        {
            if(rule.RuleType == RuleTypes.SCORE)
            {
                switch (rule.RuleOps)
                {
                    case RuleOperations.ROVNA_SE:
                        
                        return citizen.Score != int.Parse(rule.RuleValue);
                    case RuleOperations.NEROVNA_SE:
                        return citizen.Score == int.Parse(rule.RuleValue);
                    case RuleOperations.MENSI_NEZ:
                        return citizen.Score >= int.Parse(rule.RuleValue);
                    case RuleOperations.VETSI_NEZ:
                        return citizen.Score <= int.Parse(rule.RuleValue);
                    default: return true;
                }
            }
            else if (rule.RuleType == RuleTypes.NAME)
            {
                switch (rule.RuleOps)
                {
                    case RuleOperations.ROVNA_SE:
                        return !citizen.Name.Equals(rule.RuleValue);
                    case RuleOperations.NEROVNA_SE:
                        return citizen.Name.Equals(rule.RuleValue);
                    case RuleOperations.OBSAHUJE:
                        return !citizen.Name.Contains(rule.RuleValue);
                    case RuleOperations.NEOBSAHUJE:
                        return citizen.Name.Contains(rule.RuleValue);
                    case RuleOperations.ZACINA:
                        return !citizen.Name.StartsWith(rule.RuleValue);
                    case RuleOperations.KONCI:
                        return !citizen.Name.EndsWith(rule.RuleValue);
                    default : return true;
                }
            }
            return true;
        }
    }
}
