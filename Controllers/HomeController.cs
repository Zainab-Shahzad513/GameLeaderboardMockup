using Microsoft.AspNetCore.Mvc;
using GameLeaderboardMockup.Data;
using GameLeaderboardMockup.Models; 
using System.Linq;

namespace GameLeaderboardMockup.Controllers
{
    public class HomeController : Controller
    {
        // This delegate and event allow Member 3 to subscribe to score submissions 
        public delegate void ScoreSubmittedHandler(int playerId, int score);
        public static event ScoreSubmittedHandler? OnScoreSubmitted;
        private static AchievementObserver _achievementObserver = new AchievementObserver();

        public HomeController()
        {
            OnScoreSubmitted += _achievementObserver.OnScoreSubmitted;
        }


        //public IActionResult Index()
        //{
        //    // Information Expert: We get the list of players from FakeData
        //    ViewBag.Players = FakeData.Players;
        //    return View();
        //}
        public IActionResult Index()
        {
            // Sort players by PersonalBest DESCENDING
            var sortedPlayers = FakeData.Players
                .OrderByDescending(p => p.PersonalBest)
                .ToList();

            // Top player for leaderboard
            ViewBag.TopPlayer = sortedPlayers.FirstOrDefault();

            // Full leaderboard list
            ViewBag.Players = sortedPlayers;

            return View();
        }


        // Displays a specific player's achievements and stats
        public IActionResult Profile(int id)
        {
            var player = FakeData.Players.FirstOrDefault(p => p.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            ViewBag.Player = player; 
            return View();
        }

        // This action retrieves the history of all commands from the mock database
        public IActionResult AuditLog()
        {
            var logs = FakeData.AuditLog.OrderByDescending(l => l.Timestamp).ToList();
            return View(logs);
        }

        [HttpPost]
        public IActionResult SubmitScore(int playerId, int score)
        {
            // 1. VALIDATION: Ensure the score is realistic (Requirement FR-05)
            if (score < 0 || score > 1000000)
            {
                return RedirectToAction("Index");
            }

            // 2. COMMAND PATTERN: Encapsulate the request
            var scoreCommand = new SubmitScoreCommand(playerId, score);

            // Command acts as Information Expert to update Player.PersonalBest.
            scoreCommand.Execute();

            // 4. AUDIT LOGGING: Pure Fabrication
            AuditLogger.LogScore(playerId, score);

            // 5. OBSERVER NOTIFICATION: Trigger Event Hook
            // This alerts the AchievementService (Member 3) that a score was processed.
            // Observer Pattern
            var player = FakeData.Players.FirstOrDefault(p => p.Id == playerId);
            if (player != null)
            {
                // Fire the event so Member 3’s observer updates achievements
                OnScoreSubmitted?.Invoke(playerId, player.PersonalBest);
            }

            return RedirectToAction("Index");
        }
        //private void UpdateAchievements(Player player, int score)
        //{
        //    // Achievement 1: First Leaderboard Entry
        //    if (!player.Achievements.Contains("First Leaderboard Entry"))
        //    {
        //        player.Achievements.Add("First Leaderboard Entry");
        //    }

        //    // Achievement 2: Score Above 1000
        //    if (score > 1000 && !player.Achievements.Contains("Score Above 1000"))
        //    {
        //        player.Achievements.Add("Score Above 1000");
        //    }

        //    // Achievement 3: New Personal Best
        //    if (score > player.PersonalBest)
        //    {
        //        player.PersonalBest = score;

        //        if (!player.Achievements.Contains("New Personal Best"))
        //        {
        //            player.Achievements.Add("New Personal Best");
        //        }
        //    }
        //}

    }
}