using Microsoft.JSInterop;
using Newtonsoft.Json;
using RegexReplacer.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexReplacer.Client.Helper
{
    internal partial class RuleSetHelper : RegexReplacer.Shared.RuleSetHelper
    {
        private const string RulSetNames = "[RuleSetNames]";
        readonly DataSaveHelper dataSaveHelper;

        public RuleSetHelper(IJSRuntime jsRuntime)
        {
            dataSaveHelper = new DataSaveHelper(jsRuntime);
        }

        public virtual async Task LoadRuleSetsAsync()
        {
            List<string> allRuleSetNames = await GetRuleSetNames();
            var fileContents = allRuleSetNames.Select(x => dataSaveHelper.Read(x).Result);
            RuleSets = GetRuleSetsFromJson(fileContents);
        }


        public async Task<bool> SaveFileAsync(string name, Dictionary<string, string> replaceWith)
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
                    ReplaceWith = replaceWith
                };

                var names = await GetRuleSetNames();
                if (!names.Contains(name))
                {
                    names.Add(name);
                    await SetRuleSetNamesAsync(names);
                }

                await dataSaveHelper.Save(name, JsonConvert.SerializeObject(replacement));
                return true;
            }
            catch (Exception ex)
            {
                // ToDo: Notification
                return false;
            }
        }

        private async Task<List<string>> GetRuleSetNames()
        {
            var content = await dataSaveHelper.Read(RulSetNames);
            var allRuleSetNames = JsonConvert.DeserializeObject<List<string>>(content) ?? new List<string>();
            return allRuleSetNames;
        }

        private async Task SetRuleSetNamesAsync(List<string> names)
        {
            var content = JsonConvert.SerializeObject(names);
            await dataSaveHelper.Save(RulSetNames, content);
        }
    }
}
