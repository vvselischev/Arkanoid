using Microsoft.Xna.Framework;

namespace Pinball
{
    public class GameManager
    {
        private Bound bound;
        private Board<Brick> board;
        private BallManager ballManager;
        private BonusManager bonusManager;
        private LevelManager<PinballLevel> levelManager;

        public event VoidHandler OnGameOver;

        private int screenWidth;
        private int screenHeight;
        public Rectangle ScreenRect
        {
            get
            {
                return new Rectangle(0, 0, screenWidth, screenHeight);
            }
            private set
            {
                screenHeight = value.Height;
                screenWidth = value.Width;
            }
        }

        private static GameManager instance;

        public static GameManager GetInstance()
        {
            return instance ?? (instance = new GameManager());
        }

        private GameManager()
        {
        }

        private ScoreManager scoreManager;
        private LifeManager lifeManager;

        public int Lives
        {
            get
            {
                return lifeManager.Lives;
            }
            set
            {
                lifeManager.Lives = value;
            }
        }

        public Bound Bound
        {
            get
            {
                return bound;
            }

            set
            {
                bound = value;
            }
        }

        public Board<Brick> Board
        {
            get
            {
                return board;
            }

            set
            {
                board = value;
            }
        }

        public void AddScore(int score)
        {   
            scoreManager.AddScore(score);
        }

        public void Init( LevelManager<PinballLevel> levelManager,  Bound bound, Rectangle screenRect)
        {
            ballManager = BallManager.GetInstance();
            bonusManager = BonusManager.GetInstance();
            scoreManager = ScoreManager.GetInstance();
            this.bound = bound;
            lifeManager = LifeManager.GetInstance();
            ScreenRect = screenRect;           

            this.levelManager = levelManager;
        }

        public void AddBallToCenterBound()
        {
            ballManager.AddBall(new Vector2(
                        (bound.Body.Collider.Right + bound.Body.Collider.Left ) / 2 - ballManager.GameBallPrototype.Body.Size.X,
                        (bound.Body.Collider.Top - ballManager.GameBallPrototype.Body.Size.Y - 10)));
        }

        public void ActivateBonus(Bonus bonus)
        {
            bonusManager.AddBonus(bonus);
        }

        public void RemoveBall(Ball ball)
        {
            ballManager.RemoveBall(ball);
            if (ballManager.BallsCount == 0)
                Lives--;
        }

        public void CheckBallCollider(Ball ball)
        {
            levelManager.CurrentLevel.CheckBallCollider(ball, ScreenRect);
        }

        public void CheckBonusCollider(FallingBonus bonus)
        {
            levelManager.CurrentLevel.CheckBonusCollider(bonus, ScreenRect);
        }

        public void RemoveBonus(FallingBonus bonus)
        {
            bonusManager.RemoveBonus(bonus);
        }

        public void MoveToNextLevel()
        {
            ballManager.ResetBalls();
            bonusManager.ResetBonuses();

            scoreManager.AddScore(levelManager.CurrentLevelNumber * 1000);
            
            BonusBrickFactory.GetInstance().SetAllBonusesAvailable();

            if (levelManager.CurrentLevelNumber < levelManager.LevelCount)
                levelManager.MoveNext();
            else
            {
                OnGameOver?.Invoke();
            }
        }

    }
}
