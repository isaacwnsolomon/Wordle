namespace Wordle
{
    public partial class MainPage : ContentPage
    {
        private Random rng = new Random();
        private const int MaxAttempts = 5;
        private string targetWord = "HELLO";
        private int attemptCount = 0;

        public MainPage()
        {
            InitializeComponent();
        }
        private void HandleAttempt(object sender, EventArgs e)
        {
            attemptCount++;
            if (AttemptEntry.Text.ToUpper() == targetWord)
            {
                ResultLabel.Text = "Congratulations! You've guessed the word.";
            }
            else if (attemptCount <= 6)
            {
                ResultLabel.Text = "Incorrect guess, please try again.";
                var guess = AttemptEntry.Text.ToUpper();
                for (int i = 0; i < guess.Length; i++)
                {
                    var label = new Label
                    {
                        Text = guess[i].ToString(),
                        FontSize = 24,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                    };
                    GuessGrid.Children.Add(label);
                    Grid.SetRow(label, attemptCount - 1);
                    Grid.SetColumn(label, i);
                }
            }
            else
            {
                ResultLabel.Text = "Sorry, you didn't guess the word in time. The word was " + targetWord;
            }
            AttemptEntry.Text = "";     // To clear the input field
        }
    }
}