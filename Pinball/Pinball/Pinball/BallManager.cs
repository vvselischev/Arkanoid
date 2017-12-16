using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pinball
{
    public class BallManager
    {
        private List<Ball> balls;
        private GameManager gameManager;
        private bool ballCountChanged;
        private Ball gameBallPrototype;

        private static BallManager instance;
        public static BallManager GetInstance()
        {
            return instance ?? (instance = new BallManager());
        }

        public Ball GameBallPrototype
        {
            get
            {
                return gameBallPrototype.Clone();
            }

            set
            {
                gameBallPrototype = value;
            }
        }

        /*public Ball InterfaceBallPrototype
        {
            get
            {
                return ballPrototype.Clone();
            }

            set
            {
                ballPrototype = value;
                ballPrototype.Body.Transform.velocity = Vector2.Zero;
            }
        }*/

        public int BallsCount => balls.Count;

        public void Init(Ball ballPrototype)
        {
            gameManager = GameManager.GetInstance();
            balls = new List<Ball>();
            //this.InterfaceBallPrototype = ballPrototype;
            GameBallPrototype = ballPrototype;
            //AddBall(BallPrototype);
        }

        public void AddBall(Ball ball)
        {
            balls.Add(ball);
        }

        public void AddBall(Vector2 position)
        {
            Ball newBall = GameBallPrototype;
            newBall.Body.Transform.position = position;
            AddBall(newBall);
        }

        public void AddBall(Ball ball, Vector2 position)
        {
            ball.Body.Transform.position = position;
            AddBall(ball);
        }

        public void RemoveBall(Ball ball)
        {
            balls.Remove(ball);
            ball = null;
            ballCountChanged = true;
        }

        public void ResetBalls()
        {
            balls.Clear();
        }

        public void UpdateBalls(GameTime gameTime)
        {
            foreach (Ball ball in balls)
            { 
                gameManager.CheckBallCollider(ball);
                if (ballCountChanged)
                {
                    ballCountChanged = false;

                    //Обновляем оставшиеся.
                    UpdateBalls(gameTime);
                    break;
                }
                ball.Update(gameTime);
            }
        }

        public void DrawBalls(SpriteBatch spriteBatch)
        {
            foreach (Ball ball in balls)
            {
                ball.Draw(spriteBatch);
            }
        }
    }
}
