using Newtonsoft.Json;
using RegexReplacer.Shared;

namespace RegexReplacer.FormsApp
{
    public partial class FormSettings : Form
    {
        private readonly RuleSetHelper ruleSetHelper = new();

        public string RuleSetName { get => comboBoxReplacments.Text; set => comboBoxReplacments.Text = value; }

        public FormSettings()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ruleSetHelper.LoadRuleSets();
            ruleSetHelper.AddRulesetsToCollection((ObjectCollection)comboBoxReplacments.Items, true);
            UpdateGui();
        }

        private void UpdateGuiEventHandler(object sender, EventArgs e)
        {
            UpdateGui();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(ruleSetHelper.SaveFile(RuleSetName, GetValuesFromGrid()))
            {
                btnSave.BackColor = Color.LightGreen;
            }
            else
            {
                btnSave.BackColor = Color.Red;
            }
        }

        private void BtnSave_Leave(object sender, EventArgs e)
        {
            btnSave.BackColor = SystemColors.Control;
        }

        private void UpdateGui()
        {
            if (RuleSetName.ToLower() == Shared.RuleSetHelperBase.NewFile.ToLower())
            {
                RuleSetName = "";
            }
            DisplayRuleSet(ruleSetHelper.GetRuleSet(RuleSetName));
        }

        private void DisplayRuleSet(RuleSet replaceValues)
        {
            dataGridView.Rows.Clear();

            var emptyRows = Enumerable.Range(0, replaceValues.Rules.Count).Select(x => new DataGridViewRow()).ToArray();
            dataGridView.Rows.AddRange(emptyRows);

            for (int i = 0; i < replaceValues.Rules.Count; i++)
            {
                var replaceWith = replaceValues.Rules.ElementAt(i);

                dataGridView.Rows[i].Cells[0].Value = replaceWith.Replace;
                dataGridView.Rows[i].Cells[1].Value = replaceWith.With;
            }
        }

        private Dictionary<string, string> GetValuesFromGrid()
        {
            return dataGridView.Rows.Cast<DataGridViewRow>()
                                    .Where(x => x.Cells[0]?.Value != null)
                                    .Select(row => (ReadCell(row, 0), ReadCell(row, 1)))
                                    .ToDictionary(x => x.Item1, x => x.Item2);
        }

        private static string ReadCell(DataGridViewRow row, int i)
        {
            return row.Cells[i].Value?.ToString() ?? "";
        }
    }
}