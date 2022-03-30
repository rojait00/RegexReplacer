using RegexReplacer.Shared;

namespace RegexReplacer.FormsApp
{
    internal class ObjectCollection : IObjectCollection
    {
        ComboBox.ObjectCollection collection;

        public ObjectCollection(ComboBox.ObjectCollection collection)
        {
            this.collection = collection;
        }

        public void Add(string value)
        {
            collection.Add(value);
        }

        public void AddRange(IEnumerable<string> values)
        {
            collection.AddRange(values.ToArray());
        }

        public void Clear()
        {
            collection.Clear();
        }

        public static explicit operator ObjectCollection(ComboBox.ObjectCollection collection)
        {
            return new ObjectCollection(collection);
        }
    }
}