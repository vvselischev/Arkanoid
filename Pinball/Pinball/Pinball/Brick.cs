using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public abstract class Brick : IMyDrawable
    {
        protected Body body;
        private int score;

        public Body Body
        {
            get
            {
                return body;
            }

            set
            {
                body = value;
            }
        }

        protected int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
                
            }
        }

        public virtual void Action()
        {
            GameManager.GetInstance().AddScore(score);
        }

        public Brick(Body body, int score)
        {
            this.score = score;
            Body = body;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            body.Draw(spriteBatch);
        }
    }
}
