using Microsoft.Maui.Storage;

namespace Wordle
{
    public partial class PlayerEntry : ContentPage
    {
        public PlayerEntry()
        {
            InitializeComponent();
        }

        private void SaveButtonClicked(object sender, EventArgs e)
        {
            var playerName = PlayerNameEntry.Text;
            Preferences.Set("PlayerName", playerName);
        }
    }
}