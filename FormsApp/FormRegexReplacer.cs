using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace RegexReplacer.FormsApp
{
    public partial class FormRegexReplacer : Form
    {
        readonly RuleSetHelper ruleSetHelper = new ();

        public FormRegexReplacer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            LoadRuleSets();
            comboBoxRuleSets.Text = comboBoxRuleSets.Items[0].ToString();
        }

        private void Generate()
        {
            tbOutput.Text = ruleSetHelper.Generate(tbInput.Text, comboBoxRuleSets.Text);
        }

        private void BtnReload_Click(object sender, EventArgs e)
        {
            LoadRuleSets();
            Generate();
        }

        private void BtnManageRules_Click(object sender, EventArgs e)
        {
            var settings = new FormSettings
            {
                StartPosition = FormStartPosition.CenterParent
            };

            if (comboBoxRuleSets.Text != Shared.RuleSetHelper.All)
            {
                settings.RuleSetName = comboBoxRuleSets.Text;
            }

            settings.ShowDialog();
            LoadRuleSets();
        }

        private void LoadRuleSets()
        {
            ruleSetHelper.LoadRuleSets();
            ruleSetHelper.AddRulesetsToCollection((ObjectCollection)comboBoxRuleSets.Items, false);
        }

        private void InputChangedEventHandler(object sender, EventArgs e)
        {
            Generate();
        }

    }
}