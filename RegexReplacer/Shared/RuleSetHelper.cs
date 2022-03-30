using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace RegexReplacer.Shared
{
    public partial class RuleSetHelper
    {
        public const string All = "[All]";
        public const string NewFile = "[New]";

        List<RuleSet> ruleSets = new();

        public List<RuleSet> RuleSets { get => ruleSets; set => ruleSets = value; }

        public virtual void AddRulesetsToComboBox(IObjectCollection items, bool isEditMode)
        {
            items.Clear();
            items.Add(isEditMode ? NewFile : All);
            items.AddRange(RuleSets.Select(x => x.Name));
        }

        public RuleSet GetRuleSet(string name)
        {
            return RuleSets.FirstOrDefault(x => x.Name.ToLower() == name.ToLower()) ?? new RuleSet(name);
        }

        public string Generate(string content, string ruleSetName)
        {
            ruleSetName = ruleSetName.ToLower();

            var selectedRuleSets = RuleSets.Where(x => ruleSetName == All.ToLower() || ruleSetName == x.Name.ToLower())
                                           .ToList();

            var rules = selectedRuleSets.SelectMany(x => x.ReplaceWith)
                                        .ToList();

            rules.ForEach(x => content = Replace(content, x.Key, x.Value));

            return content;
        }

        public static RuleSet GetRuleSetFromJson(string x)
        {
            return JsonConvert.DeserializeObject<RuleSet>(x) ?? new RuleSet(true);
        }

        private static string Replace(string input, string replace, string with)
        {
            return Regex.Replace(input, replace, with, RegexOptions.IgnoreCase);
        }
    }
}