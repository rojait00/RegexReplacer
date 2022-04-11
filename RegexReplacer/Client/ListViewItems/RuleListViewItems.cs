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

        public RuleSet RuleSet
        {
            get => ruleSet;
            set
            {
                ruleSet = value;
                ItemToInsert = null;
            }
        }

        public override async Task InsertRow()
        {
            if (!RuleSet.IsReadOnly)
            {
                await base.InsertRow();
            }
        }

        public async Task Init(RuleSetHelper helper, NotificationService notificationService)
        {
            this.helper = helper;
            this.notificationService = notificationService;
            ruleSet = helper?.RuleSets.FirstOrDefault() ?? RuleSet.GetDemoRuleSet();
            await LoadItems();
        }

        public override async Task DeleteRow(Rule item)
        {
            await base.DeleteRow(item);
            await OnChangedCollectionChanged(item);
        }

        public override async Task LoadItems()
        {
            Items = ruleSet.Rules;
            await base.LoadItems();
        }

        public override async Task OnChangedCollectionChanged(Rule? rule)
        {
            if (helper != null && notificationService != null)
            {
                await helper.SaveRuleSetAsync(RuleSet, notificationService);
            }

            await base.OnChangedCollectionChanged(rule);
        }
    }
}
