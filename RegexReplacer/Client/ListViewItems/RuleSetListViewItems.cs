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
            Items = helper?.RuleSets ?? new List<RuleSet>();
            await base.LoadItems();
        }

        public override async Task OnChangedCollectionChanged()
        {
            // ToDo: impl.
            await base.OnChangedCollectionChanged();
        }
    }
}
