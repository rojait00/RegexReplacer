namespace RegexReplacer
{
    internal class ReplaceValues
    {
        public string Name { get; set; } = "";

        public ReplaceValues(string name)
        {
            Name = name;
        }

        public ReplaceValues()
        {
        }

        public Dictionary<string, string> Replacements { get; set; } = new();
    }
}