using Radzen;
using RadzenHelper;
using RegexReplacer.Client.Helper;
using RegexReplacer.Shared;

namespace RegexReplacer.Client.ListViewItems
{
    internal class RuleSetListViewItems : ListViewItemsBase<RuleSet>
    {
        private RuleSetHelper helper;
        private readonly NotificationService notificationService;

        public RuleSetListViewItems(RuleSetHelper helper, NotificationService notificationService)
        {

            this.helper = helper;
            this.notificationService = notificationService;
            LoadItems();
        }
        public override void LoadItems()
        {
            Items = helper.GetRuleSetNames()
                             .Select(x => helper.GetRuleSet(x))
                             .ToList();
        }

        public override async Task OnChangedCollectionChanged()
        {
            // ToDo: impl.
        }
    }
}
