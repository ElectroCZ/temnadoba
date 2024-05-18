using Temna_Doba.DayGroup;
using Temna_Doba.RuleGroup;

namespace Temna_Doba.InputGroup
{
    internal class InputAnalyser
    {
        private int ruleCount = 0;
        private int currentDayRuleCount = 0;
        private int currentDayCitizenCount = 0;
        private int ruleDayLineCount = 0;
        private bool hasDayBeenProcessed = false;
        private InputReceiver receiver;
        private RuleRepository ruleRepository;
        private List<string> currentDayRuleIdentificators = new List<string>();
        private List<Citizen> currentDayCitizenList = new List<Citizen>();
        private DayRuleRepository dayRuleRepository;

        public InputAnalyser(DayRuleRepository dayRuleRepository,RuleRepository ruleRepository)
        {
            receiver = new InputReceiver();
            this.ruleRepository = ruleRepository;
            this.dayRuleRepository = dayRuleRepository;
            
        }

        public void AnalyseInput()
        {
            while (!receiver.HasInputEnded()) 
            {
                string getNextInLine = receiver.GetNextInLine();
                if (string.IsNullOrWhiteSpace(getNextInLine))
                {
                    break;
                }
                
                string[] splitLine = getNextInLine.Split(' ');
                int lineCount = receiver.LinesRead;

                if (lineCount == 1)
                {
                    ruleCount = int.Parse(splitLine[0]);
                    continue;
                }
                else if (lineCount <= ruleCount + 1)
                {
                    Rule rule = new Rule();
                    rule.Identificator = splitLine[0];
                    rule.RuleType = ConvertStringToRuleType(splitLine[1]);
                    rule.RuleOps = ConvertStringToRuleOps(splitLine[2]);
                    rule.RuleValue = splitLine[3];
                    ruleRepository.AddRule(rule);
                    continue;
                }
                else
                {
                    ruleDayLineCount++;
                    
                    if (ruleDayLineCount >  currentDayRuleCount + currentDayCitizenCount 
                        && currentDayCitizenList.Count != 0
                        && currentDayRuleIdentificators.Count != 0)
                    {
                        EndDay(currentDayCitizenList,currentDayRuleIdentificators);
                        ClearDayData();
                        hasDayBeenProcessed = false;
                    }
                    
                    if (!hasDayBeenProcessed)
                    {
                        currentDayRuleCount = int.Parse(splitLine[0]);
                        currentDayCitizenCount = int.Parse(splitLine[1]);
                        
                        hasDayBeenProcessed = true;
                        ruleDayLineCount = 0;
                        
                        continue;
                    }
                    
                    if (ruleDayLineCount <= currentDayRuleCount)
                    {
                        currentDayRuleIdentificators.Add(splitLine[0]);
                        continue;
                    }
                    
                    if (ruleDayLineCount <= currentDayRuleCount + currentDayCitizenCount)
                    {
                        Citizen citizen = new Citizen();
                        citizen.Score = int.Parse(splitLine[splitLine.Length - 1]);
                        
                        string citizenName = string.Empty;
                        for (int i = 0; i < splitLine.Length - 1; i++)
                        {
                            citizenName += splitLine[i] + " ";
                        }
                        citizenName.TrimEnd();

                        citizen.Name = citizenName;
                        currentDayCitizenList.Add(citizen);
                        continue;
                    }
                }
            }
            EndDay(currentDayCitizenList,currentDayRuleIdentificators);
            ClearDayData();
        }
        
        private void EndDay(List<Citizen> citizens, List<string> ruleIdentificators)
        {
            DayRule dayRule = new DayRule();
            dayRule.CitizenList = new List<Citizen>(citizens);
            dayRule.RuleIdentificatorList = new List<string>(ruleIdentificators);

            dayRuleRepository.AddDayRule(dayRule);
        }

        private void ClearDayData()
        {
            currentDayCitizenList.Clear();
            currentDayRuleIdentificators.Clear();
            ruleDayLineCount = 0;
        }

        private RuleTypes ConvertStringToRuleType(string ruleType)
        {
            switch(ruleType)
            {
                case "name":
                    return RuleTypes.NAME;

                case "score":
                    return RuleTypes.SCORE;

                default:
                    return RuleTypes.NAME;
            }
        }

        private RuleOperations ConvertStringToRuleOps(string ruleOps)
        {
            switch (ruleOps)
            {
                case "rovnaSe":
                    return RuleOperations.ROVNA_SE;
                case "nerovnaSe":
                    return RuleOperations.NEROVNA_SE;
                case "obsahuje":
                    return RuleOperations.OBSAHUJE;
                case "neobsahuje":
                    return RuleOperations.NEOBSAHUJE;
                case "zacina":
                    return RuleOperations.ZACINA;
                case "konci":
                    return RuleOperations.KONCI;
                case "mensiNez":
                    return RuleOperations.MENSI_NEZ;
                case "vetsiNez":
                    return RuleOperations.VETSI_NEZ;

                default:
                    return RuleOperations.ROVNA_SE;
            }
        } 
    }
}