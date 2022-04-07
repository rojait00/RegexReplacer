using Radzen;
using RadzenHelper;
using RegexReplacer.Client.Helper;
using RegexReplacer.Shared;

namespace RegexReplacer.Client.ListViewItems
{
    internal class RuleListViewItems : ListViewItemsBase<Rule>
    {
        private RuleSetHelper? helper;
        private NotificationService? notificationService;
        private RuleSet ruleSet = new();


        public string Name { get => RuleSet.Name; set => RuleSet.Name = value; }

        public RuleSet RuleSet { get => ruleSet; set => ruleSet = value; }

        public async Task Init(RuleSetHelper helper, NotificationService notificationService)
        {
            this.helper = helper;
            ruleSet = helper.RuleSets.FirstOrDefault() ?? new();
            this.notificationService = notificationService;
            await LoadItems();
        }

        public override async Task LoadItems()
        {
            RuleSet = helper?.GetRuleSet(RuleSet.Id) ?? new();
            Items = ruleSet.Rules;
            await base.LoadItems();
        }

        public override async Task OnChangedCollectionChanged()
        {
            if (helper != null && notificationService != null)
            {
                await helper.SaveRuleSetAsync(RuleSet, notificationService);
            }
        }
    }
}
