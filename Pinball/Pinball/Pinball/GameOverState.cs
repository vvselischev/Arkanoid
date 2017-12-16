using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public class GameOverState : GameState
    {
        public Game1 gameToOver;
        private GameOverInterface gui;
        private ScoreManager scoreManager;

        public GameOverState()
        {
            scoreManager = ScoreManager.GetInstance();
            gui = new GameOverInterface(scoreManager.Score, scoreManager.HighScore, GameManager.GetInstance().ScreenRect);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            gui.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
