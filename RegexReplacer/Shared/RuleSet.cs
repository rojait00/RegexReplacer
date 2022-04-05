using Newtonsoft.Json;

namespace RegexReplacer.Shared
{
    public class RuleSet
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

        [Obsolete]
        public Dictionary<string, string> ReplaceWith
        {
            get { return Rules.ToDictionary(x => x.Replace, x => x.With); }
            set { Rules = value.Select(x => new Rule(x.Key, x.Value, RegexFunction.Replace)).ToList(); }
        }

        public IList<Rule> Rules { get; set; } = new List<Rule>();

        public Guid Id { get; set; } = Guid.NewGuid();

        public void Update(RuleSet ruleSet)
        {
            Name = ruleSet.Name;
            Rules = ruleSet.Rules;
        }
    }
}