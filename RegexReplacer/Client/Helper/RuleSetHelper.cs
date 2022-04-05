using Microsoft.JSInterop;
using Newtonsoft.Json;
using Radzen;
using RegexReplacer.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexReplacer.Client.Helper
{
    internal partial class RuleSetHelper : RegexReplacer.Shared.RuleSetHelperBase
    {
        private const string RulSetNamesCookie = "[RuleSetNames]";
        readonly DataSaveHelper dataSaveHelper;

        public RuleSetHelper(IJSRuntime jsRuntime)
        {
            dataSaveHelper = new DataSaveHelper(jsRuntime);
        }

        public virtual async Task LoadRuleSetsAsync()
        {
            List<string> allRuleSetNames = await LoadRuleSetNames();
            var fileContents = allRuleSetNames.Select(x => dataSaveHelper.Read(x).Result);
            RuleSets = GetRuleSetsFromJson(fileContents);
        }


        public async Task<bool> SaveFileAsync(string name, IList<Rule> rules, NotificationService notificationService)
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
                    Rules = rules
                };

                var names = await LoadRuleSetNames();
                if (!names.Contains(name))
                {
                    names.Add(name);
                    await SetRuleSetNamesAsync(names);
                }

                await dataSaveHelper.Save(name, JsonConvert.SerializeObject(replacement));
                ShowSucessMeesage(name, notificationService);
                return true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(notificationService, ex);
                return false;
            }
        }

        private static void ShowErrorMessage(NotificationService notificationService, Exception ex)
        {
            var message = new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Could not save Rule Set!",
                Detail = ex.Message,
                Duration = 9000
            };
            notificationService.Notify(message);
        }

        private static void ShowSucessMeesage(string name, NotificationService notificationService)
        {
            var message = new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = "Rule Set saved!",
                Detail = $"The rule set \"{name}\" has been saved.",
                Duration = 3000
            };
            notificationService.Notify(message);
        }

        private async Task<List<string>> LoadRuleSetNames()
        {
            var content = await dataSaveHelper.Read(RulSetNamesCookie);
            var allRuleSetNames = JsonConvert.DeserializeObject<List<string>>(content) ?? new List<string>();
            return allRuleSetNames;
        }

        private async Task SetRuleSetNamesAsync(List<string> names)
        {
            var content = JsonConvert.SerializeObject(names);
            await dataSaveHelper.Save(RulSetNamesCookie, content);
        }
    }
}
