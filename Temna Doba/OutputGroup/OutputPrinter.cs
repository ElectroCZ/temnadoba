using Temna_Doba.DayGroup;

namespace Temna_Doba.OutputGroup
{
    public class OutputPrinter
    {
        private DayRuleManager dayRuleManager;

        public OutputPrinter(DayRuleManager dayRuleManager)
        {
            this.dayRuleManager = dayRuleManager;
        }

        public void OutputPrint()
        {
            List<BrokenDayRuleOutput> brokenDayRuleOutputs = dayRuleManager.GetBrokenDayRules();

            Console.Clear();
            
            foreach(BrokenDayRuleOutput brokenDayRule in brokenDayRuleOutputs)
            {
                Console.WriteLine($"DEN {brokenDayRule.Day}");
                foreach((string citizenName, string brokenRule) brokenRuleAndCitizen in brokenDayRule.CitizenBrokenRules)
                {
                    Console.WriteLine($"{brokenRuleAndCitizen.citizenName} (z pravidla {brokenRuleAndCitizen.brokenRule})");
                }
            }
        }
    }
}