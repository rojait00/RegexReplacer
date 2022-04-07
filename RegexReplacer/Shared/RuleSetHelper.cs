using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace RegexReplacer.Shared
{
    public partial class RuleSetHelperBase
    {
        List<RuleSet> ruleSets = new();

        public List<RuleSet> RuleSets { set => ruleSets = value; }
        public List<string> RuleSetNames { get => ruleSets.Select(x => x.Name).ToList(); }

        public IEnumerable<RegexOptions> SelectedRegexOptions { get; set; } = new List<RegexOptions>() { RegexOptions.IgnoreCase };

        public List<string> GetRuleSetIds()
        {
            var newItems = ruleSets.Select(x => x.Name).ToList();
            return newItems;
        }

        public static List<RuleSet> GetRuleSetsFromJson(IEnumerable<string> jsons)
        {
            var rules = jsons.Select(x => JsonConvert.DeserializeObject<RuleSet>(x) ?? new RuleSet(true))
                             .Where(x => !x.IsNull)
                             .ToList();
            return rules;
        }

        public RuleSet GetRuleSet(string id)
        {
            return ruleSets.FirstOrDefault(x => x.Id.ToString() == id.ToLower()) ?? new RuleSet();
        }



        public string Generate(string content, IEnumerable<string> ruleSetNames)
        {
            var lowerNames = ruleSetNames.Select(x => x.ToLower()).ToList();
            var result = content;
            lowerNames.ForEach(x => result = Generate(result, x));
            return result;

        }

        public string Generate(string content, string ruleSetName)
        {
            ruleSetName = ruleSetName.ToLower();

            var rules = ruleSets.Where(x => ruleSetName == x.Name.ToLower())
                                .SelectMany(x => x.Rules)
                                .ToList();

            rules.ForEach(x => content = Generate(content, x.Replace, x.With, x.Function));

            return content;
        }

        private string Generate(string input, string replace, string with, RegexFunction function)
        {
            try
            {
                RegexOptions options = RegexOptions.None;
                SelectedRegexOptions.ToList().ForEach(x => options |= x);

                var result = input;
                switch (function)
                {
                    case RegexFunction.Replace:
                        result = Regex.Replace(input, replace, with, options);
                        break;
                    case RegexFunction.List:
                        var matches = Regex.Matches(input, replace, options);
                        var newValues = matches.Cast<Match>().ToList()
                                                .Select(x => Regex.Replace(x.Value, replace, with, options));
                        result = string.Join("", newValues);
                        break;
                    default:
                        break;
                }
                return result;
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }
    }
}