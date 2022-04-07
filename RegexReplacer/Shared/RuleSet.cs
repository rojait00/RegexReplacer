using Newtonsoft.Json;
using RadzenHelper;
namespace RegexReplacer.Shared
{
    public class RuleSet : UpdatableBase
    {
        /// <summary>
        /// Should be used in Code
        /// </summary>
        /// <param name="name"></param>
        public RuleSet(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Needed for JsonConvert
        /// </summary>
        public RuleSet()
        { }


        /// <summary>
        /// Used to avoid Warning caused by JsonConvert
        /// </summary>
        /// <param name="isNull"></param>
        internal RuleSet(bool isNull)
        {
            IsNull = isNull;
        }

        public string Name { get; set; } = "";

        public bool IsNull { get; } = false;

        public IList<Rule> Rules { get; set; } = new List<Rule>();

        public static RuleSet GetDemoRuleSet()
        {
            return new RuleSet
            {
                Id = Guid.Empty,
                Name = "Demo",
                Rules = new List<Rule> {
                    new Rule("test","test1", RegexFunction.Replace),
                    new Rule("a","b", RegexFunction.Replace),
                    new Rule("12\\d456","123456", RegexFunction.Replace)
                }
            };
        }
    }
}