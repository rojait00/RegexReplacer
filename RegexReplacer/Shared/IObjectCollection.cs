namespace RegexReplacer.Shared
{
    public interface IObjectCollection
    {
        void Clear();
        void Add(string value);
        void AddRange(IEnumerable<string> values);
    }
}