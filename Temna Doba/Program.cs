using Temna_Doba.DayGroup;
using Temna_Doba.InputGroup;
using Temna_Doba.OutputGroup;
using Temna_Doba.RuleGroup;

namespace Temna_Doba
{
    public class Program
    {
        public static void Main()
        {
            RuleRepository ruleRepository = new RuleRepository();
            DayRuleRepository dayRuleRepository = new DayRuleRepository();
            
            DayRuleManager dayRuleManager = new DayRuleManager(dayRuleRepository, ruleRepository);
            
            InputAnalyser inputAnalyser = new InputAnalyser(dayRuleRepository,ruleRepository);
            OutputPrinter outputPrinter = new OutputPrinter(dayRuleManager);

            inputAnalyser.AnalyseInput();
            outputPrinter.OutputPrint();
            Console.ReadKey();
        }
    }
}