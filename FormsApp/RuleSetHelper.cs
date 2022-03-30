using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexReplacer.FormsApp
{
    internal partial class RuleSetHelper : Shared.RuleSetHelper
    {
        public virtual string Path { get => "C:\\Replacements"; }

        public virtual void LoadRuleSets()
        {
            Directory.CreateDirectory(Path);
            RuleSets = Directory.GetFiles(Path)
                                .Select(x => File.ReadAllText(x))
                                .Select(x => GetRuleSetFromJson(x))
                                .Where(x => !x.IsNull)
                                .ToList();

        }
    }
}
