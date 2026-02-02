namespace GameLeaderboardMockup.Models
{
    public static class AuditLogger
    {
        public static void LogScore(int playerId, int score)
        {
            IScoreCommand logEntry = new SubmitScoreCommand(playerId, score);
            Data.FakeData.AuditLog.Add(logEntry);
        }
    }
}