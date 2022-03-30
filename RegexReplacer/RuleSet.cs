namespace RegexReplacer
{
    internal class RuleSet
    {
        public string Name { get; set; } = "";

        public bool IsNull { get; } = false;

        public Dictionary<string, string> ReplaceWith { get; set; } = new();

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

        /// <summary>
        /// Used to avoid Warning caused by JsonConvert
        /// </summary>
        /// <param name="isNull"></param>
        internal RuleSet(bool isNull)
        {
            IsNull = isNull;
        }
       
    }
}