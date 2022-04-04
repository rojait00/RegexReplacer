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
        public List<string> RuleSetNames { get => ruleSets.Select(x => x.Name).ToList(); }

        public IEnumerable<RegexOptions> SelectedRegexOptions { get; set; } = new List<RegexOptions>() { RegexOptions.IgnoreCase };


        public virtual void AddRulesetsToCollection(IObjectCollection items, bool isEditMode)
        {
            List<string> newItems = GetRuleSetNames(isEditMode);

            items.Clear();
            items.AddRange(newItems);
        }

        public List<string> GetRuleSetNames(bool isEditMode)
        {
            var newItems = ruleSets.Select(x => x.Name).ToList();
            newItems.Add(isEditMode ? NewFile : All);
            return newItems;
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

        public string Generate(string content, IEnumerable<string> ruleSetNames)
        {
            var lowerNames = ruleSetNames.Select(x => x.ToLower()).ToList();
            if (lowerNames.Contains(All.ToLower()))
            {
                return Generate(content, All);
            }
            else
            {
                var result = content;
                lowerNames.ForEach(x => result = Generate(result, x));
                return result;
            }

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

        private string Replace(string input, string replace, string with)
        {
            try
            {
                RegexOptions options = RegexOptions.None;
                SelectedRegexOptions.ToList().ForEach(x => options |= x);

                return Regex.Replace(input, replace, with, options);
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }
    }
}