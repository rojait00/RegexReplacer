using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexReplacer.Shared
{
    public static class EnumHelper
    {
        public static List<T> GetEnums<T>() where T : Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));
            return values.ToList();
        }
    }
}
