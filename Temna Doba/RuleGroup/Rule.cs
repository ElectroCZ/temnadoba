
using System.Security.Principal;

namespace Temna_Doba.RuleGroup
{
    public class Rule
    {
        public string Identificator { get; set; }
        public RuleTypes RuleType { get; set; }
        public RuleOperations RuleOps { get; set; }
        public string RuleValue { get; set; }
        
    }
}
