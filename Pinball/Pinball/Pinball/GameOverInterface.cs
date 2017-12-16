using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public class GameOverInterface : UInterface
    {
        private int score;
        private int highScore;

        public GameOverInterface(int score, int highScore, Rectangle screen) : base(screen)
        {
            this.score = score;
            this.highScore = highScore;
            font = MyContentManager.GetInstance().fonts["InterfaceFont"];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
                spriteBatch.DrawString(font, "GAME OVER!",
                    new Vector2(ScreenWidth * 0.25f, ScreenHeight * 0.1f), Color.Red, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, "Your score: " + score,
                    new Vector2(ScreenWidth * 0.1f, ScreenHeight * 0.4f), Color.Gold, 0, Vector2.Zero, 4f, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, "Highscore: " + highScore,
                    new Vector2(ScreenWidth * 0.1f, ScreenHeight * 0.6f), Color.Gold, 0, Vector2.Zero, 4f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

    }
}
