using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace RegexReplacer.Shared
{
    public partial class RuleSetHelper
    {
        public const string All = "[All]";
        public const string NewFile = "[New]";

        List<RuleSet> ruleSets = new();

        public List<RuleSet> RuleSets { set => ruleSets = value; }

        public virtual void AddRulesetsToCollection(IObjectCollection items, bool isEditMode)
        {
            items.Clear();
            items.Add(isEditMode ? NewFile : All);
            items.AddRange(ruleSets.Select(x => x.Name));
        }

        public static List<RuleSet> GetRuleSetsFromJson(IEnumerable<string> jsons)
        {
            var rules = jsons.Select(x => JsonConvert.DeserializeObject<RuleSet>(x) ?? new RuleSet(true))
                             .Where(x => !x.IsNull)
                             .ToList();
            return rules;
        }

        public RuleSet GetRuleSet(string name)
        {
            return ruleSets.FirstOrDefault(x => x.Name.ToLower() == name.ToLower()) ?? new RuleSet(name);
        }

        public string Generate(string content, string ruleSetName)
        {
            ruleSetName = ruleSetName.ToLower();

            var rules = ruleSets.Where(x => ruleSetName == All.ToLower() || ruleSetName == x.Name.ToLower())
                                .SelectMany(x => x.ReplaceWith)
                                .ToList();

            rules.ForEach(x => content = Replace(content, x.Key, x.Value));

            return content;
        }

        private static string Replace(string input, string replace, string with)
        {
            try
            {
                return Regex.Replace(input, replace, with, RegexOptions.IgnoreCase);
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }
    }
}