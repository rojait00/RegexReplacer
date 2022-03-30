using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace RegexReplacer
{
    public partial class FormRegexReplacer : Form
    {
        readonly RuleSetHelper ruleSetHelper = new ();

        public FormRegexReplacer()
        {
            InitializeComponent();
        }

        private void InputChanged(object sender, EventArgs e)
        {
            tbOutput.Text = ruleSetHelper.Generate(tbInput.Text, comboBoxRuleSets.Text);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            LoadRuleSets();
            comboBoxRuleSets.Text = comboBoxRuleSets.Items[0].ToString();
        }


        private void BtnReload_Click(object sender, EventArgs e)
        {
            LoadRuleSets();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            var settings = new FormSettings();
            
            if(comboBoxRuleSets.Text != RuleSetHelper.All)
            {
                settings.RuleSetName = comboBoxRuleSets.Text;
            }

            settings.ShowDialog();
            LoadRuleSets();
        }

        private void LoadRuleSets()
        {
            ruleSetHelper.LoadRuleSets(comboBoxRuleSets.Items, this);
        }
    }
}