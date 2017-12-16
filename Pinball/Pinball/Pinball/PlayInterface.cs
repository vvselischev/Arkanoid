using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Pinball
{
    public class PlayInterface : UInterface
    {
        private Vector2 livesTextPosition;
        private Vector2 scoreTextPosition;
        private Sprite lifeBall;

        public Sprite LifeBall
        {
            get
            {
                return lifeBall;
            }
            set
            {
                lifeBall = value;
            }
        }
        private Stack<Sprite> lifeBalls;
       

        public Vector2 LivesTextRightPosition
        {
            get
            {
                return livesTextPosition;
            }

            set
            {
                livesTextPosition = value;
            }
        }

        public Vector2 ScoreTextPosition
        {
            get
            {
                return scoreTextPosition;
            }

            set
            {
                scoreTextPosition = value;
            }
        }

        public int Score { get; set; }


        public PlayInterface(Rectangle screen) : base(screen)
        {
            font = MyContentManager.GetInstance().fonts["InterfaceFont"];
            lifeBalls = new Stack<Sprite>();
            scoreTextPosition = new Vector2(0, 0);
            
        }

        public void Initialize(Sprite lifeBall)
        {
            LifeBall = lifeBall;
        }

        public void LivesChanged(int newCount)
        {
            int delta = newCount - lifeBalls.Count;
            if (delta > 0)
            {
                for (int i = 0; i < delta; i++)
                {
                    lifeBalls.Push(lifeBall.Clone());
                }
            }
            else
            {
                for (int i = 0; i < -delta; i++)
                {
                    lifeBalls.Pop();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            livesTextPosition = new Vector2(ScreenWindow.Width, 0);
            for (int i = 0; i < lifeBalls.Count; i++)
            {
                Sprite currentBall = lifeBalls.ElementAt(i);
                currentBall.Transform.position = new Vector2(livesTextPosition.X -
                    (i + 1) * currentBall.Size.X, livesTextPosition.Y);
                currentBall.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
              spriteBatch.DrawString(font, "Score: " + Score.ToString(), scoreTextPosition, Color.Gold);
            spriteBatch.End();

            for (int i = 0; i < lifeBalls.Count; i++)
            {
                Sprite currentBall = lifeBalls.ElementAt(i);

                currentBall.Draw(spriteBatch);
            }
            
        }

    }
}
