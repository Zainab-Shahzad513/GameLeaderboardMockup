using GameLeaderboardMockup.Models; // Step 1: Add this to access the Command interface

namespace GameLeaderboardMockup.Data
{
    public static class FakeData
    {
        public static List<Player> Players = new()
        {
            new Player
            {
                Id = 1,
                Name = "Ali",
                PersonalBest = 1200,
                Achievements = new List<string> { "First Leaderboard Entry", "Score Above 1000", "New Personal Best" }
            },
            new Player
            {
                Id = 2,
                Name = "Sara",
                PersonalBest = 850,
                Achievements = new List<string> { "First Leaderboard Entry", "New Personal Best" }
            },
            new Player
            {
                Id = 3,
                Name = "Ahmed",
                PersonalBest = 400,
                Achievements = new List<string> { "First Leaderboard Entry" }
            }
        };
      
        // This list acts as our mock database table for "Audit Records".

        public static List<IScoreCommand> AuditLog = new List<IScoreCommand>();
    }

    public class Player
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int PersonalBest { get; set; }
        public List<string> Achievements { get; set; } = new(); 
    }
}