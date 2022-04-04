namespace RegexReplacer.Shared
{
    public class DisplayEnumHelper<EnumType> where EnumType : struct, Enum, IComparable, IConvertible, IFormattable
    {
        private EnumType option;

        public static EnumType DefaultValue = default;

        public DisplayEnumHelper(EnumType option)
        {
            this.option = option;
        }

        public EnumType Option { get => option; set => option = value; }

        public int Value
        {
            get { return GetValue(option); }
            set { option = GetByValue(value); }
        }

        public string Name
        {
            get { return option.ToString(); }
            set { option = GetByName(value); }
        }


        public static EnumType GetByName(string name)
        {
            Enum.TryParse(typeof(EnumType), name, true, out object? value);
            return (EnumType?)value ?? DefaultValue;
        }

        public static EnumType GetByValue(int value)
        {
            var values = (EnumType[])Enum.GetValues(typeof(EnumType));
            var results = values.Where(x => value == GetValue(x));

            if(results.Any())
            {
                return results.First();
            }
            return DefaultValue;
        }

        public static int GetValue(EnumType? option)
        {
            return (int)(object)(option ?? DefaultValue);
        }

        public static implicit operator DisplayEnumHelper<EnumType>(EnumType value)
        {
            return new DisplayEnumHelper<EnumType>(value);
        }

        public static implicit operator EnumType(DisplayEnumHelper<EnumType> value)
        {
            return value.Option;
        }
    }
}
