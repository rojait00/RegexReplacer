
namespace RegexReplacer.Shared
{
    public class Rule 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string Replace { get; set; } = "";
        
        public string With { get; set; } = "";

        public RegexFunction Function { get; set; } = RegexFunction.Replace;

        public Rule() 
        {

        }
        public Rule(Rule rule)
        {
            Update(rule);
        }

        public void Update(Rule rule)
        {
            Replace = rule.Replace;
            With = rule.With;
            Function = rule.Function;
        }

        public Rule(string replace, string with, RegexFunction regexFunction)
        {
            Replace = replace;
            With = with;
            Function = regexFunction;
        }

        public static implicit operator KeyValuePair<string, string>(Rule value)
        {
            return new KeyValuePair<string, string>(value.Replace, value.With);
        }
    }

    public static class RuleExtensions
    {
        public static Dictionary<string, string> GetDictionary(this IList<Rule> values)
        {
            return values.ToDictionary(value => value.Replace, value => value.With);
        }
        public static IList<Rule> FromDictionary(this Dictionary<string, string> values)
        {
            return values.Select(x => new Rule(x.Key, x.Value, RegexFunction.Replace)).ToList();
        }
    }
}
