using Newtonsoft.Json;
using RegexReplacer.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexReplacer.FormsApp
{
    internal partial class RuleSetHelper : Shared.RuleSetHelperBase
    {
        public virtual string Path { get => "C:\\Replacements"; }

        public virtual void LoadRuleSets()
        {
            Directory.CreateDirectory(Path);
            var fileContents = Directory.GetFiles(Path)
                                .Select(x => File.ReadAllText(x));
            RuleSets = GetRuleSetsFromJson(fileContents);
        }

        public bool SaveFile(string name, Dictionary<string, string> replaceWith)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            try
            {
                var replacement = new RuleSet
                {
                    Name = name,
                    Rules = replaceWith.Select(x => new Rule(x.Key, x.Value, RegexFunction.Replace)).ToList()
                };

                var path = System.IO.Path.Combine(Path, replacement.Name + ".json");
                File.WriteAllText(path, JsonConvert.SerializeObject(replacement));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
