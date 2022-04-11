using Radzen;
using RadzenHelper;
using RegexReplacer.Client.Helper;
using RegexReplacer.Shared;

namespace RegexReplacer.Client.ListViewItems
{
    internal class RuleSetListViewItems : ListViewItemsBase<RuleSet>
    {
        private RuleSetHelper? helper;
        private NotificationService? notificationService;

        public async Task Init(RuleSetHelper helper, NotificationService notificationService)
        {
            this.helper = helper;
            this.notificationService = notificationService;
            await LoadItems();
        }

        public override async Task LoadItems()
        {
            Items = helper?.RuleSets.ToList() ?? new List<RuleSet>();
            await base.LoadItems();
            ItemToInsert = null;
        }

        public override async Task DeleteRow(RuleSet ruleSet)
        {
            await base.DeleteRow(ruleSet);

            if (helper != null && notificationService != null && ruleSet != null)
            {
                await helper.DeleteRuleSetAsync(ruleSet, notificationService);
            }
            await base.OnChangedCollectionChanged(ruleSet);
        }

        public override async Task OnChangedCollectionChanged(RuleSet? ruleSet)
        {
            if (helper != null && notificationService != null && ruleSet != null)
            {
                await helper.SaveRuleSetAsync(ruleSet, notificationService);
            }
            await base.OnChangedCollectionChanged(ruleSet);
        }
    }
}
