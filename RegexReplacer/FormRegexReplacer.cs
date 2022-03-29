using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace RegexReplacer
{
    public partial class FormRegexReplacer : Form
    {
        private const string Path = "C:\\Replacements";
        private const string All = "All";
        List<ReplaceValues> replacements = new();

        public FormRegexReplacer()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            var selectedReplacments = replacements.Where(x => comboBoxReplacments.Text == All || comboBoxReplacments.Text == x.Name);
            tbOutput.Text = StartReplacing(tbInput.Text, selectedReplacments.ToList());
        }

        private string StartReplacing(string text, List<ReplaceValues> replaceValues)
        {
            var result = text;
            replaceValues.ForEach(replacement => replacement.Replacements.ToList().ForEach(x => result = Replace(result, x.Key, x.Value)));
            return result;
        }

        private string Replace(string input, string replace, string with)
        {
            return Regex.Replace(input, replace, with, RegexOptions.IgnoreCase);
        }

        private void Form1_Load(object sender, EventArgs e)
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
            comboBoxReplacments.Items.Add(All);
            comboBoxReplacments.Items.AddRange(replacements.Select(x => x.Name).ToArray());
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settings = new FormSettings();
            settings.ShowDialog();
            Reload();
        }

        private void comboBoxReplacments_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}