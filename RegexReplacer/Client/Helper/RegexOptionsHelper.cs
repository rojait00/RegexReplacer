using RegexReplacer.Shared;
using System.Text.RegularExpressions;

namespace RegexReplacer.Client.Helper
{
    public class RegexOptionsHelper : DisplayEnumHelper<RegexOptions>
    {
        public RegexOptionsHelper(RegexOptions option) : base(option)
        {
            DefaultValue = RegexOptions.None;
        }
    }
}
