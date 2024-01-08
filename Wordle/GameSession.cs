using Microsoft.Maui.Storage;
namespace Wordle
{
    public class GameSession
    {
        //Each property to represent the information 
        public string playerName { get; set; }
        public DateTime timeStamp { get; set; }
        public string correctWord { get; set; }
        public int guessTaken { get; set; }
        //Constructor to set up the game
        public GameSession(string playerName, string correctWord, int guessesTaken)
        {
            this.playerName = playerName;
            this.correctWord = correctWord;
            this.guessTaken = guessesTaken;
            // sets the timeStamp when a new Session is created.
            timeStamp = DateTime.Now;
        }
        //Load data from all game sessions from save file
        public static List<GameSession> LoadGameSessions()
        {
            //Defining path to saved file 
            string saveFilePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "save.json");

            if (File.Exists(saveFilePath))
            {
                //Read JSON file 
                string jsonData = File.ReadAllText(saveFilePath);
                List<GameSession> gameSessions = new List<GameSession>();
                var document = System.Text.Json.JsonDocument.Parse(jsonData);
                //Converts JSON data to game objects
                foreach (var sessionElement in document.RootElement.EnumerateArray())
                {
                    gameSessions.Add(new GameSession(
                 sessionElement.GetProperty("playerName").GetString(),
                 sessionElement.GetProperty("correctWord").GetString(),
                 sessionElement.GetProperty("guessesTaken").GetInt32())
                    {
                        //Sets timestamp for each session 
                        timeStamp = sessionElement.GetProperty("TimeStamp").GetDateTime(),
                    });

                }

                return gameSessions;
            }
            //If file doesnt exist return an emtpy list 
            return new List<GameSession>();
        }

        //Save a new game by adding it to other saved games and upating the saved file 
        public static void SaveGameSession(GameSession newSession)
        {
            List<GameSession> allSessions = LoadGameSessions();
            allSessions.Add(newSession);

            string jsonData = "[";
            foreach (GameSession session in allSessions)
            {
                jsonData += "{";
                jsonData += $"\"PlayerName\":\"{session.playerName}\",";
                jsonData += $"\"CorrectWord\":\"{session.correctWord}\",";
                jsonData += $"\"GuessesTaken\":{session.guessTaken},";
                jsonData += $"\"TimeStamp\":\"{session.timeStamp.ToString("s")}\"";
                jsonData += "},";
            }
            jsonData = jsonData.TrimEnd(',') + "]";
            //Write JSON file data to the file 
            File.WriteAllText(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "save.json"), jsonData);
        }
    }
}