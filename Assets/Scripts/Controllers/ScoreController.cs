using Interfaces;

namespace Controllers {
	public class ScoreController : IScoreController {

		int currentScore;

		public int CurrentScore {
			get {
				return currentScore;
			}

			private set {
				currentScore = value;
			}
		}

		public void AddScore(int score) {
			CurrentScore += score;
		}

		public void SetScore(int score) {
			CurrentScore = score;
		}
	}
}
