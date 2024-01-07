using System.ComponentModel;
using System.Net;


namespace Wordle
{
    public partial class MainPage : ContentPage
    {
        private Random rng = new Random();
        private const int MaxAttempts = 6;
        private string targetWord = "";
        private int attemptCount = 0;
        private string localFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "words.txt");

        public MainPage()
        {
            InitializeComponent();

            // Check if file exists in local storage
            if (!File.Exists(localFilePath))
            {
                // If not, download it
                WebClient client = new WebClient();
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(FileDownloadCompleted);
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/main/words.txt"), localFilePath);
            }
            else
            {
                // If file exists, set the random target word
                targetWord = GetRandomWord();
            }
            AttemptButton.BackgroundColor = Color.FromHex("#F64C72");
            AttemptButton.TextColor = Microsoft.Maui.Graphics.Colors.White;
            AttemptButton.CornerRadius = 5;
        }
        private void HandleAttempt(object sender, EventArgs e)
        {


            var guess = AttemptEntry.Text.ToUpper();


            if (guess == targetWord)
            {
                ResultLabel.Text = "Congratulations! You've guessed the word.";
            }
            else if (attemptCount <= MaxAttempts)
            {
                ResultLabel.Text = "Incorrect guess, please try again.";
                for (int i = 0; i < guess.Length; i++)
                {
                    //Create a new label for each character
                    var label = new Label
                    {
                        Text = guess[i].ToString(),
                        FontSize = 24,
                    };

                    //Create a new frame for the label
                    var frame = new Frame
                    {
                        Content = label,
                        //Rounding the corners
                        CornerRadius = 10,
                        //making padding between the frame and the label
                        Padding = new Thickness(10),
                    };

                    //Check the guess against the target word 
                    if (targetWord[i] == guess[i])  // Right letter, right place
                        frame.BackgroundColor = Colors.Green;
                    else if (targetWord.Contains(guess[i]))  // Right letter, wrong place
                        frame.BackgroundColor = Colors.Orange;
                    else  // Wrong letter
                        frame.BackgroundColor = Colors.DarkGray;

                    //Add the frame to the Grid
                    GuessGrid.Children.Add(frame);
                    Grid.SetRow(frame, attemptCount);
                    Grid.SetColumn(frame, i);
                }

            }
            else
            {
                ResultLabel.Text = "Sorry, you didn't guess the word in time. The word was " + targetWord;
            }

            AttemptEntry.Text = "";  // To clear the input field
            attemptCount++;

        }
        void FileDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Set the target word once the file has been downloaded
            targetWord = GetRandomWord();
        }

        public string GetRandomWord()
        {
            string[] words = File.ReadAllLines(localFilePath);
            int randomIndex = rng.Next(words.Length);
            return words[randomIndex].ToUpper();
        }
        public void newGame(object sender, EventArgs e)
        {
            targetWord = GetRandomWord();
            attemptCount = 0;
            GuessGrid.Children.Clear();
            ResultLabel.Text = "";
            AttemptEntry.Text = "";
        }


    }
}

//Fix user being able to guess more than 6 times 