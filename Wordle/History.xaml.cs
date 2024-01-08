using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace Wordle
{
    public partial class History : ContentPage
    {
        public History(string playerName)
        {
            InitializeComponent();
            List<GameSession> gameHistory = GameSession.LoadGameSessions();

            // Filter the history for the current player
            gameHistory = gameHistory.Where(session => session.playerName == playerName).ToList();

            GameHistoryList.ItemsSource = gameHistory;
        }
    }
}