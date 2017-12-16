using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pinball
{
    public class PinballLevel : ILevel
    {
        private GameManager gameManager;
        private Board<Brick> board;
        private Bound bound;

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

        public List<Rectangle> Colliders { get; set; }

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

        public PinballLevel(Board<Brick> board, Bound bound, List<Rectangle> colliders)
        {
            this.board = board;
            this.Colliders = colliders;
            gameManager = GameManager.GetInstance();
            this.Bound = bound;
        }

        public void Start()
        {
            gameManager.Board = Board;
            bound.SetDefaultWidth();
            gameManager.AddBallToCenterBound();
        }

        public void CheckBallCollider(Ball ball, Rectangle clientWindow)
        {
            if (ball.Body.Collider.Bottom > bound.Body.Collider.Top + 4)
            {
                gameManager.RemoveBall(ball);
                return;
            }

            if (ball.Body.Collider.Intersects(bound.Body.Collider))
            {
                if (!bound.Body.IntersectsVert(ball.Body.Collider))
                    ball.Body.Transform.velocity.Y = -ball.Body.Transform.velocity.Y;
            }

            ball.CheckOutOfScreen(clientWindow);


            foreach (Brick brick in board.ActiveValues)
            {
                if (ball.Body.Collider.Intersects(brick.Body.Collider))
                {
                    if (brick.Body.IntersectsVert(ball.Body.Collider))
                        ball.Body.Transform.velocity.X = -ball.Body.Transform.velocity.X;
                    else
                        ball.Body.Transform.velocity.Y = -ball.Body.Transform.velocity.Y;

                    brick.Action();
                    board.Remove(brick);
                    break;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            board.Update();
            bound.Update(gameTime);

            if (board.ActiveValues.Count == 0)
                gameManager.MoveToNextLevel();
        }

        public void Draw(SpriteBatch sb)
        {
            board.DrawAll(sb);
            bound.Draw(sb);
        }

        public void CheckBonusCollider(FallingBonus bonus, Rectangle screen)
        {
            if (bonus.Body.Collider.Bottom > bound.Body.Collider.Top)
            {
                gameManager.RemoveBonus(bonus);
            }
            if (bonus.Body.Collider.Intersects(bound.Body.Collider))
            {
                if (!bound.Body.IntersectsVert(bonus.Body.Collider))
                    bonus.Invoke();
            }
        }
    }
}