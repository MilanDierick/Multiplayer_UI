using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MULTIPLAYER.UI.Extensions;
using MULTIPLAYER.UI.Settings;

namespace MULTIPLAYER.UI.Views
{
    public partial class SettingsView : Form
    {
        private GlobalSettings GlobalSettings { get; set; }

        public SettingsView()
        {
            GlobalSettings = new GlobalSettings();

            InitializeComponent();
        }

        private void TextBoxSetting_TextChanged(object sender, System.EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox?.Name == null) return;

            typeof(GlobalSettings).GetProperty(textBox.Name)?.SetValue(GlobalSettings, textBox.Text, null);
        }

        private void ButtonSave_Click(object sender, System.EventArgs e)
        {
            GlobalSettings.SaveSettingsToXmlFile();
        }

        private void ButtonLoad_Click(object sender, System.EventArgs e)
        {
            GlobalSettings.LoadSettingsFromXmlFile();

            foreach (TextBox textBox in tabPageSettings.Controls.OfType<TextBox>())
                textBox.Text = (string) GlobalSettings.GetPropertyValue(textBox.Name);
        }
    }
}
