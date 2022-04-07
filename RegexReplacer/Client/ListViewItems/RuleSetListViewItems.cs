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
            Items = helper.RuleSets;
        }

        public override async Task OnChangedCollectionChanged()
        {
            // ToDo: impl.
            await base.OnChangedCollectionChanged();
        }
    }
}
