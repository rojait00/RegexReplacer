using Newtonsoft.Json;

namespace RegexReplacer
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
            ruleSetHelper.LoadRuleSets(comboBoxReplacments.Items, this);
            UpdateGui();
        }

        private void UpdateGuiEventHandler(object sender, EventArgs e)
        {
            UpdateGui();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void UpdateGui()
        {
            if (RuleSetName == RuleSetHelper.NewFile)
            {
                RuleSetName = "";
            }
            DisplayRuleSet(ruleSetHelper.GetRuleSet(RuleSetName));
        }

        private void DisplayRuleSet(RuleSet replaceValues)
        {
            dataGridView.Rows.Clear();
            
            var emptyRows = Enumerable.Range(0, replaceValues.ReplaceWith.Count).Select(x => new DataGridViewRow()).ToArray();
            dataGridView.Rows.AddRange(emptyRows);

            for (int i = 0; i < replaceValues.ReplaceWith.Count; i++)
            {
                var replaceWith = replaceValues.ReplaceWith.ElementAt(i);

                dataGridView.Rows[i].Cells[0].Value = replaceWith.Key;
                dataGridView.Rows[i].Cells[1].Value = replaceWith.Value;
            }
        }

        private void SaveFile()
        {
            try
            {
                var replacement = new RuleSet
                {
                    Name = comboBoxReplacments.Text,
                    ReplaceWith = GetValuesFromGrid()
                };

                var path = Path.Combine(RuleSetHelper.Path, replacement.Name + ".json");
                File.WriteAllText(path, JsonConvert.SerializeObject(replacement));
                MessageBox.Show("File has been saved.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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