using Microsoft.JSInterop;
using Newtonsoft.Json;
using Radzen;
using RegexReplacer.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexReplacer.Client.Helper
{
    internal partial class RuleSetHelper
    {
        private const string RulSetNamesCookie = "_RuleSetNames_";
        DataSaveHelper dataSaveHelper;
        List<RuleSet> ruleSets = new();

        public List<RuleSet> RuleSets { get => ruleSets; }

        private RuleSetHelper(DataSaveHelper dataSaveHelper)
        {
            this.dataSaveHelper = dataSaveHelper;
        }

        public static async Task<RuleSetHelper> GetRuleSetHelper(IJSRuntime jSRuntime) => await GetRuleSetHelper(new DataSaveHelper(jSRuntime));
        public static async Task<RuleSetHelper> GetRuleSetHelper(DataSaveHelper dataSaveHelper)
        {
            var helper = new RuleSetHelper(dataSaveHelper);
            await helper.LoadRuleSetsAsync();
            return helper;
        }

        public RuleSet GetRuleSet(Guid id)
        {
            return ruleSets.FirstOrDefault(x => x.Id == id) ?? new RuleSet();
        }

        public async Task<bool> DeleteRuleSetAsync(RuleSet ruleSet, NotificationService notificationService)
        {
            if (ruleSet.Id == Guid.Empty)
            {
                return true;
            }

            try
            {
                ruleSets.RemoveAll(x => x.Id == ruleSet.Id);
                await WriteRuleSetIdsAsync();
                await dataSaveHelper.Delete(ruleSet.Id.ToString());
                return true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(notificationService, ex);
                return false;
            }
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

        public List<string> GetRuleSetNames()
        {
            return ruleSets.Select(x => x.Name)
                           .Where(x => string.IsNullOrWhiteSpace(x))
                           .ToList();
        }

        private async Task<List<Guid>> LoadRuleSetIds()
        {
            var content = await dataSaveHelper.Read(RulSetNamesCookie) ?? "";
            var allRuleSetIds = JsonConvert.DeserializeObject<List<Guid>>(content) ?? new List<Guid>();
            return allRuleSetIds;
        }

        private async Task WriteRuleSetIdsAsync()
        {
            var ids = ruleSets.Select(x => x.Id)
                              .Where(x => x != Guid.Empty)
                              .ToList();

            var content = JsonConvert.SerializeObject(ids);
            await dataSaveHelper.Save(RulSetNamesCookie, content);
        }

        private async Task LoadRuleSetsAsync()
        {
            var allRuleSetIds = await LoadRuleSetIds();

            var tasks = allRuleSetIds.Select(x => dataSaveHelper.Read(x.ToString()));

            var contents = await Task.WhenAll(tasks);

            ruleSets = contents.Select(x => JsonConvert.DeserializeObject<RuleSet>(x ?? "") ?? new RuleSet())
                               .Where(x => !x.IsNull)
                               .ToList();

            if (!ruleSets.Any())
            {
                ruleSets.Add(RuleSet.GetDemoRuleSet());
            }
        }

        private static void ShowErrorMessage(NotificationService notificationService, Exception ex)
        {
            var message = new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "There was an error!",
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

        public string Generate(string content, List<RuleSet> ruleSets, IEnumerable<RegexOptions> selectedRegexOptions)
        {
            var result = content;
            var rules = ruleSets.SelectMany(x => x.Rules)
                                .ToList();
            rules.ForEach(x => result = Replace(result, x, selectedRegexOptions));
            return result;

        }

        private static string Replace(string input, Rule rule, IEnumerable<RegexOptions> selectedRegexOptions)
        {
            try
            {
                RegexOptions options = RegexOptions.None;
                selectedRegexOptions.ToList().ForEach(x => options |= x);

                rule.With = rule.With.Replace("\\n", "\n");


                var result = input;
                if (rule.Function == RegexFunction.List)
                {
                    var values = Regex.Matches(input, rule.Replace, options).Cast<Match>().Select(x => x.Value);
                    values = values.Select(input => EnhancedReplace(input, rule.Replace, rule.With, options));
                    result = string.Join("", values);
                }
                else if (rule.Function == RegexFunction.Replace)
                {
                    result = EnhancedReplace(input, rule.Replace, rule.With, options);
                }

                return result;
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }

        private static string EnhancedReplace(string input, string pattern, string replacement, RegexOptions options)
        {
            replacement = Regex.Replace(replacement, @"(?<mode>\\[UL])(?<group>\$((\d+)|({[^}]+})))", @"<!<mode:${mode}>%&${group}&%>");
            var output = Regex.Replace(input, pattern, replacement, options);
            output = Regex.Replace(output, @"<!<mode:\\L>%&(?<value>[\w\W]*?)&%>", x => x.Groups["value"].Value.ToLower());
            output = Regex.Replace(output, @"<!<mode:\\U>%&(?<value>[\w\W]*?)&%>", x => x.Groups["value"].Value.ToUpper());
            return output;
        }
    }
}
