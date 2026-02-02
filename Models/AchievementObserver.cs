using GameLeaderboardMockup.Data;
using System.Linq;

namespace GameLeaderboardMockup.Models
{
    public class AchievementObserver
    {
        // This method matches the delegate in HomeController
        public void OnScoreSubmitted(int playerId, int score)
        {
            var player = FakeData.Players.FirstOrDefault(p => p.Id == playerId);
            if (player == null)
                return;

            //First Leaderboard Entry
            if (!player.Achievements.Contains("First Leaderboard Entry"))
            {
                player.Achievements.Add("First Leaderboard Entry");
            }

            //Score Above 1000
            if (score > 1000 && !player.Achievements.Contains("Score Above 1000"))
            {
                player.Achievements.Add("Score Above 1000");
            }

            //New Personal Best
            if (score >= player.PersonalBest && !player.Achievements.Contains("New Personal Best"))
                player.Achievements.Add("New Personal Best");

        }
    }
}
