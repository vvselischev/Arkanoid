using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public class PlayState : GameState
    {
        private BonusManager bonusManager;
        private LevelManager<PinballLevel> levelManager;
        private BallManager ballManager;
        private ScoreManager scoreManager;
        private PlayInterface gui;
        private LifeManager lifeManager;
        private GameManager gameManager;


        public PlayState(int lives)
        {
            bonusManager = BonusManager.GetInstance();
            levelManager = LevelManager<PinballLevel>.GetInstance();
            ballManager = BallManager.GetInstance();
            scoreManager = ScoreManager.GetInstance();
            gameManager = GameManager.GetInstance();
            stateManager = StateManager.GetInstance();

            gui = new PlayInterface(GameManager.GetInstance().ScreenRect);
            gui.Initialize(ballManager.GameBallPrototype.Sprite.Clone());
            gui.LivesChanged(lives);
            lifeManager = LifeManager.GetInstance();
            lifeManager.Lives = lives;
            lifeManager.OnLivesChanged += LivesChanged;

            gameManager.OnGameOver += OnGameOver;


            levelManager.SetLevel(1);
        }

        private void OnGameOver()
        {
            stateManager.CurrentState = new GameOverState();
        }

        private void LivesChanged()
        {
            int value = lifeManager.Lives;

            if (value == 0)
            {
                stateManager.CurrentState = new GameOverState();
            }

           gui.LivesChanged(lifeManager.Lives);

            if ((value > 0) && (ballManager.BallsCount == 0))
                gameManager.AddBallToCenterBound();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            levelManager.CurrentLevel.Draw(spriteBatch);
            bonusManager.Draw(spriteBatch);
            gui.Draw(spriteBatch);
            ballManager.DrawBalls(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            bonusManager.Update(gameTime);
            ballManager.UpdateBalls(gameTime);
            levelManager.CurrentLevel.Update(gameTime);

            gui.Score = scoreManager.Score;
            gui.Update(gameTime);
        }
    }
}
