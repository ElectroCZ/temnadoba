
namespace Temna_Doba.RuleGroup
{
    public class RuleRepository
    {
        private List<Rule> ruleList;

        public RuleRepository()
        {
            ruleList = new List<Rule>();
        }
        public void AddRule(Rule rule)
        {
            ruleList.Add(rule);
        }
    }
}
