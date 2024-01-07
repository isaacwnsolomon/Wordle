using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using Microsoft.Maui.Controls;

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
        }
        private void HandleAttempt(object sender, EventArgs e)
        {
            attemptCount++;
            var guess = AttemptEntry.Text.ToUpper();  // Moved this up, so it's declared only once

            if (guess == targetWord)
            {
                ResultLabel.Text = "Congratulations! You've guessed the word.";
            }
            else if (attemptCount <= MaxAttempts)
            {
                ResultLabel.Text = "Incorrect guess, please try again.";

                for (int i = 0; i < guess.Length; i++)
                {
                    var label = new Label
                    {
                        Text = guess[i].ToString(),
                        FontSize = 24,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                    };
                    // Check the guess against the target word
                    if (targetWord[i] == guess[i])  // Right letter, right place
                        label.BackgroundColor = Color.FromRgba(0, 255, 0, 1);
                    else if (targetWord.Contains(guess[i]))  // Right letter, wrong place
                        label.BackgroundColor = Color.FromRgba(251, 192, 147, 1);
                    else  // Wrong letter
                        label.BackgroundColor = Color.FromRgba(108, 122, 137, 1);

                    GuessGrid.Children.Add(label);
                    Grid.SetRow(label, attemptCount - 1);
                    Grid.SetColumn(label, i);
                }
            }
            else
            {
                ResultLabel.Text = "Sorry, you didn't guess the word in time. The word was " + targetWord;
            }

            AttemptEntry.Text = "";  // To clear the input field
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


    }
}

