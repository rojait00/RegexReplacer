using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace RegexReplacer
{
    internal class RuleSetHelper
    {
        public const string Path = "C:\\Replacements";
        public const string All = "[All]";
        public const string NewFile = "[New]";

        List<RuleSet> ruleSets = new();

        public void LoadRuleSets(ComboBox.ObjectCollection items, Form parent)
        {
            Directory.CreateDirectory(Path);
            ruleSets = Directory.GetFiles(Path)
                                .Select(x => File.ReadAllText(x))
                                .Select(x => GetRuleSetFromJson(x))
                                .Where(x => !x.IsNull)
                                .ToList();

            items.Clear();
            
            var isMainForm = parent.GetType() == typeof(FormRegexReplacer);
            items.Add(isMainForm ? All : NewFile);
            items.AddRange(ruleSets.Select(x => x.Name).ToArray());
        }

        public RuleSet GetRuleSet(string name)
        {
            return ruleSets.FirstOrDefault(x => x.Name.ToLower() == name.ToLower()) ?? new RuleSet(name);
        }

        public string Generate(string content, string ruleSetName)
        {
            var selectedRuleSets = ruleSets.Where(x => ruleSetName == All || ruleSetName == x.Name)
                                                       .ToList();
            var rules = selectedRuleSets.SelectMany(x => x.ReplaceWith)
                                        .ToList();
            rules.ForEach(x => content = Replace(content, x.Key, x.Value));
            return content;
        }

        private static RuleSet GetRuleSetFromJson(string x)
        {
            return JsonConvert.DeserializeObject<RuleSet>(x) ?? new RuleSet(true);
        }

        private static string Replace(string input, string replace, string with)
        {
            return Regex.Replace(input, replace, with, RegexOptions.IgnoreCase);
        }
    }
}