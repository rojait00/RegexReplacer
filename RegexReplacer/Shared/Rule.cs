using RadzenHelper;

namespace RegexReplacer.Shared
{
    public class Rule : UpdatableBase
    {
        public Rule()
        {

        }
        
        public Rule(string replace, string with, RegexFunction regexFunction)
        {
            Replace = replace;
            With = with;
            Function = regexFunction;
        }

        [ColumnDefinition("Replace", IsRequired = true)]
        public string Replace { get; set; } = "";

        [ColumnDefinition("With")]
        public string With { get; set; } = "";

        [ColumnDefinition("Function")]
        public RegexFunction Function { get; set; } = RegexFunction.Replace;
    }
}
