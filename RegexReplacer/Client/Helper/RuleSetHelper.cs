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
    internal partial class RuleSetHelper
    {
        private const string RulSetNamesCookie = "[RuleSetNames]";
        readonly DataSaveHelper dataSaveHelper;
        List<RuleSet> ruleSets = new();

        public List<RuleSet> RuleSets { get => ruleSets; }

        public RuleSetHelper(IJSRuntime jsRuntime)
        {
            dataSaveHelper = new DataSaveHelper(jsRuntime);
            LoadRuleSetsAsync().Wait();
        }

        public RuleSet GetRuleSet(Guid id)
        {
            return ruleSets.FirstOrDefault(x => x.Id == id) ?? new RuleSet();
        }

        public async Task<bool> SaveRuleSetAsync(RuleSet ruleSet, NotificationService notificationService)
        {
            if (ruleSet.Id == Guid.Empty)
            {
                return true;
            }

            try
            {
                var oldRuleSet = ruleSets.FirstOrDefault(x => x.Id == ruleSet.Id);
                if (oldRuleSet != null)
                {
                    oldRuleSet.Update(ruleSet);
                }
                else
                {
                    ruleSets.Add(ruleSet);
                    await WriteRuleSetIdsAsync();
                }

                await dataSaveHelper.Save(ruleSet.Id.ToString(), JsonConvert.SerializeObject(ruleSet));
                ShowSucessMeesage(ruleSet.Name, notificationService);
                return true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(notificationService, ex);
                return false;
            }
        }

        private async Task<List<Guid>> LoadRuleSetIds()
        {
            var content = await dataSaveHelper.Read(RulSetNamesCookie);
            var allRuleSetIds = JsonConvert.DeserializeObject<List<Guid>>(content) ?? new List<Guid>();
            return allRuleSetIds;
        }

        private async Task WriteRuleSetIdsAsync()
        {
            var ids = ruleSets.Select(x => x.Id).ToList();
            var content = JsonConvert.SerializeObject(ids);
            await dataSaveHelper.Save(RulSetNamesCookie, content);
        }

        private async Task LoadRuleSetsAsync()
        {
            var allRuleSetIds = await LoadRuleSetIds();

            var tasks = allRuleSetIds.Select(x => dataSaveHelper.Read(x.ToString()));

            var contents = await Task.WhenAll(tasks);

                ruleSets = contents.Select(x => JsonConvert.DeserializeObject<RuleSet>(x) ?? new RuleSet())
                                   .Where(x => !x.IsNull)
                                   .ToList();
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
    }
}
