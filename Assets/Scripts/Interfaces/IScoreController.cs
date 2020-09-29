namespace Interfaces {
	public interface IScoreController {
		int CurrentScore { get; }
		void AddScore(int score);
		void SetScore(int score);
	}
}
