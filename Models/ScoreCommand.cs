using GameLeaderboardMockup.Data;

namespace GameLeaderboardMockup.Models
{

    public interface IScoreCommand
    {
        int PlayerId { get; }
        int Score { get; }
        DateTime Timestamp { get; }
        void Execute();
    }


    public class SubmitScoreCommand : IScoreCommand
    {
        public int PlayerId { get; }
        public int Score { get; }
        public DateTime Timestamp { get; }

        public SubmitScoreCommand(int playerId, int score)
        {
            PlayerId = playerId;
            Score = score;
            Timestamp = DateTime.Now;
        }

        public void Execute()
        {
            var player = FakeData.Players.FirstOrDefault(p => p.Id == PlayerId);
            
            if (player != null)
            {
                // Logic: Only update if the new score is better than the existing Personal Best.
                if (Score > player.PersonalBest)
                {
                    player.PersonalBest = Score;
                }
            }
        }
    }
}