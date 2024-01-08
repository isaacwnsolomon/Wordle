using Microsoft.Maui.Storage;

namespace Wordle
{
    public partial class PlayerEntry : ContentPage
    {
        //Constructor 
        public PlayerEntry()
        {
            InitializeComponent();
        }

        //Method handles when save button is clicked on login page
        private void SaveButtonClicked(object sender, EventArgs e)
        {
            //Gets the text from the PlayerNameEntry input field 
            var playerName = PlayerNameEntry.Text;
            //Saves value to app preferences with key of PlayerName
            Preferences.Set("PlayerName", playerName);
        }
    }
}