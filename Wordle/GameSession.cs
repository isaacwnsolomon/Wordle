public class GameSession
{
    public string playerName { get; set; }
    public DateTime timeStamp { get; set; }
    public string correctWord { get; set; }
    public int guessTaken { get; set; }

    public GameSession(string playerName, string correctWord, int guessesTaken)
    {
        this.playerName = playerName;
        this.correctWord = correctWord;
        this.guessTaken = guessesTaken;
        // sets the timeStamp when a new Session is created.
        timeStamp = DateTime.Now;
    }
    public static List<GameSession> LoadGameSessions()
    {
        string saveFilePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "save.json");

        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);
            List<GameSession> gameSessions = new List<GameSession>();
            var document = System.Text.Json.JsonDocument.Parse(jsonData);
            foreach (var sessionElement in document.RootElement.EnumerateArray())
            {
                gameSessions.Add(new GameSession(
             sessionElement.GetProperty("playerName").GetString(),
             sessionElement.GetProperty("correctWord").GetString(),
             sessionElement.GetProperty("guessesTaken").GetInt32())
                {
                    timeStamp = sessionElement.GetProperty("TimeStamp").GetDateTime(),
                });

            }

            return gameSessions;
        }

        return new List<GameSession>();
    }

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

        File.WriteAllText(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "save.json"), jsonData);
    }
}