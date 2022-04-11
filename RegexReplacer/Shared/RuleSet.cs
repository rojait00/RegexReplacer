using Newtonsoft.Json;
using RadzenHelper;
namespace RegexReplacer.Shared
{
    public class RuleSet : UpdatableBase
    {
        /// <summary>
        /// Should be used in Code
        /// </summary>
        /// <param name="id"></param>
        public RuleSet(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Needed for JsonConvert
        /// </summary>
        public RuleSet()
        { }

        [ColumnDefinition("Name", IsRequired = true)]
        public string Name { get; set; } = "";

        [JsonIgnore]
        [ColumnDefinition("Rules", DisplayMode = DisplayMode.Details, IsReadOnly = true)]
        public int RuleCount { get => Rules.Count; }

        [JsonIgnore]
        public bool IsNull { get; } = false;

        public List<Rule> Rules { get; set; } = new ();

        public static RuleSet GetDemoRuleSet()
        {
            return new RuleSet
            {
                Id = Guid.Empty,
                Name = "Demo",
                IsReadOnly = true,
                Rules = new List<Rule> {
                    new Rule("test","test1", RegexFunction.Replace, true),
                    new Rule("a","b", RegexFunction.Replace, true),
                    new Rule("12\\d456","123456", RegexFunction.Replace, true)
                }
            };
        }

        public static new string GetDisplayName()
        {
            return "Rule Set";
        }
    }
}