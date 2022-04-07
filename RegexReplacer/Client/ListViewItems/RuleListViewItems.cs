using Radzen;
using RadzenHelper;
using RegexReplacer.Client.Helper;
using RegexReplacer.Shared;

namespace RegexReplacer.Client.ListViewItems
{
    internal class RuleListViewItems : ListViewItemsBase<Rule>
    {
        private readonly RuleSetHelper helper;
        private readonly NotificationService notificationService;
        private RuleSet ruleSet = new();

        [Obsolete("Please use the constructor with the full rueSet as parameter.")]

        public RuleListViewItems(RuleSetHelper helper, string name, NotificationService notificationService)
        {
            
            this.helper = helper;
            Name = name;
            this.notificationService = notificationService;
            LoadItems();
        }

        public string Name { get => RuleSet.Name; set => RuleSet.Name = value; }

        public RuleSet RuleSet { get => ruleSet; set => ruleSet = value; }

        public override void LoadItems()
        {
            RuleSet = helper.GetRuleSet(this.Name);
        }

        public override async Task OnChangedCollectionChanged()
        {
            await helper.SaveFileAsync(Name, Items, notificationService);
        }
    }
}
