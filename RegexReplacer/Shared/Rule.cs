using RadzenHelper;

namespace RegexReplacer.Shared
{
    public class Rule : UpdatableBase
    {
        /// <summary>
        /// Needed for JsonConvert
        /// </summary>
        public Rule()
        {

        }
        
        public Rule(string replace, string with, RegexFunction regexFunction)
        {
            Replace = replace;
            With = with;
            Function = regexFunction;
        }
        public Rule(string replace, string with, RegexFunction regexFunction, bool isReadOnly) : this(replace, with, regexFunction)
        {
            IsReadOnly = isReadOnly;
        }

        [ColumnDefinition("Replace", IsRequired = true)]
        public string Replace { get; set; } = "";

        [ColumnDefinition("With")]
        public string With { get; set; } = "";

        [ColumnDefinition("Function")]
        public RegexFunction Function { get; set; } = RegexFunction.Replace;

        public static new string GetDisplayName()
        {
            return "Rule";
        }
    }
}
