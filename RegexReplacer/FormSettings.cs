using Newtonsoft.Json;

namespace RegexReplacer
{
    public partial class FormSettings : Form
    {
        private const string Path = "C:\\Replacements";

        List<ReplaceValues> replacements = new();

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            Reload();

        }

        private void Reload()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            var files = Directory.GetFiles(Path);
            replacements = files.Select(x => File.ReadAllText(x))
                                .Select(x => JsonConvert.DeserializeObject<ReplaceValues>(x))
                                .Where(x => x != null)
                                .ToList();

            comboBoxReplacments.Items.Clear();
            comboBoxReplacments.Items.AddRange(replacements.Select(x => x.Name).ToArray());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var replacement = new ReplaceValues
                {
                    Name = comboBoxReplacments.Text,
                    Replacements = GetValues()
                };
                File.WriteAllText(GetPath(replacement.Name), JsonConvert.SerializeObject(replacement));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Dictionary<string, string> GetValues()
        {
            return dataGridView.Rows.Cast<DataGridViewRow>()
                                    .Where(x => x.Cells[0]?.Value != null)
                                    .Select(x => ReadRow(x))
                                    .ToDictionary(x => x.Key, x => x.Value);
        }

        private static KeyValuePair<string, string> ReadRow(DataGridViewRow x)
        {
            return new KeyValuePair<string, string>(x.Cells[0].Value.ToString(), x.Cells[1].Value?.ToString() ?? "");
        }

        private static string GetPath(string name)
        {
            return System.IO.Path.Combine(Path, name + ".json");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBoxReplacments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = comboBoxReplacments.Text;
            var path = GetPath(name);
            if (File.Exists(path))
            {
                Display(JsonConvert.DeserializeObject<ReplaceValues>(File.ReadAllText(path)));
            }
            else
            {
                Display(new ReplaceValues(Name = name));
            }
        }

        private void Display(ReplaceValues replaceValues)
        {
            dataGridView.Rows.Clear();
            dataGridView.Rows.AddRange(Enumerable.Range(0, replaceValues.Replacements.Count).Select(x => new DataGridViewRow()).ToArray());
            replaceValues.Replacements.Select((x, i) => CreateRow(i, x)).ToArray();
        }

        private bool CreateRow(int i, KeyValuePair<string, string> value)
        {
            dataGridView.Rows[i].Cells[0].Value = value.Key;
            dataGridView.Rows[i].Cells[1].Value = value.Value;
            return true;
        }
    }
}