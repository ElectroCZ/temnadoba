namespace Temna_Doba.DayGroup
{
    public class DayRuleRepository
    {
        private Dictionary<int, DayRule> dayRuleDictionary;

        public DayRuleRepository()
        {
            dayRuleDictionary = new Dictionary<int, DayRule>();
        }

        public void AddDayRule(DayRule dayRule)
        {
            int dayNumber = dayRuleDictionary.Count + 1;
            dayRuleDictionary.Add(dayNumber, dayRule);
        }
        public Dictionary<int, DayRule> GetDayRuleDictionary()
        {
            return dayRuleDictionary;
        }
    }
}