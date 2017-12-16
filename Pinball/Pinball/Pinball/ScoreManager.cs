namespace Pinball
{
    public class ScoreManager
    {
        private int score;
        private int highScore;

        private const string highScoreFilePath = "Content\\PinballHighScore.txt";

        private FileManager fileManager;

        private static ScoreManager instance;
        public static ScoreManager GetInstance()
        {
            return instance ?? (instance = new ScoreManager());
        }

        private ScoreManager()
        {
            fileManager = FileManager.GetInstance();

            fileManager.CreateIfNotExist(highScoreFilePath);
            int? tmpHighScore = fileManager.ReadFromFile(highScoreFilePath);
            if (tmpHighScore != null)
            {
                highScore = (int)tmpHighScore;
            }
            else
            {
                HighScore = 0;
            }          
        }

        private void UpdateHighScore()
        {
            fileManager.RewriteIntInFile(highScore, highScoreFilePath);
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
                if (score > highScore)
                {
                    HighScore = score;
                }
            }
        }

        public int HighScore
        {
            get
            {
                return highScore;
            }
            private set
            {
                highScore = value;
                UpdateHighScore();
            }
        }

        public void AddScore(int value)
        {
            Score += value;
        }
    }
}
