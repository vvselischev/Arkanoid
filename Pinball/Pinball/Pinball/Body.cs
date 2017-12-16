using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public class Body
    {
        protected Transform transform;
        protected Point size;
        private System.Drawing.Color color;
        protected Texture2D texture;
        protected int frameCollision;

        private Rectangle collider;


        public Texture2D Texture
        {
            get
            {
                return texture;
            }

            set
            {
                texture = value;
            }
        }

        public Point Size
        {
            get
            {
                return new Point((int)(size.X * transform.Scale), (int)(size.Y * transform.Scale));
            }

            set
            {
                size = value;
            }
        }

        public Rectangle Collider
        {
            get
            {
                Vector2 position = transform.position;
                float scale = transform.Scale;
                collider = new Rectangle((int)(position.X + frameCollision * scale), (int)(position.Y + frameCollision * scale),
                    (int)((size.X - frameCollision * 2) * scale), (int)((size.Y - frameCollision * 2) * scale));
                return collider;
            }
        }

        public Transform Transform
        {
            get
            {
                return transform;
            }

            set
            {
                transform = value;
            }
        }

        public System.Drawing.Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public virtual bool OutOfScreen(Rectangle clientWindow)
        {
            Vector2 position = transform.position;
            return (position.X < 0) || (position.X + size.X > clientWindow.Width) ||
                    (position.Y < 0) || (position.Y > clientWindow.Height);
        }

        public Body(System.Drawing.Color color, Transform transform, Point size, int frameCollision = 0)
        {
            Color = color;
            this.transform = transform;
            this.size = size;
            this.frameCollision = frameCollision;
        }

        public Body(Transform transform, Point size)
        {
            Transform = transform;
            Size = size;
            Color = System.Drawing.Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
              spriteBatch.Draw(texture, new Rectangle((int)transform.position.X, (int)transform.position.Y, size.X, size.Y), 
                new Color(color.R, color.G, color.B, color.A));
            spriteBatch.End();
        }

        /// <summary>
        /// Возвращет true, если коллайдер объекта пересекается с прямоугольником преимущественно по вертикали.
        /// </summary>
        /// <param name="ball"></param>
        /// <returns></returns>
        public bool IntersectsVert(Rectangle ball)
        {
            Rectangle brick = collider;

            //
            if (brick.Contains(new Point(ball.Left, ball.Top)))
            {
                if (brick.Contains(new Point(ball.Left, ball.Bottom)))
                    return true;
                return (brick.Bottom - ball.Top) > (brick.Right - ball.Left);
            }

            //
            if (brick.Contains(new Point(ball.Left, ball.Bottom)))
            {
                return ((ball.Bottom - brick.Top) > (brick.Right - ball.Left));
            }

            //
            if (brick.Contains(new Point(ball.Right, ball.Top)))
            {
                if (brick.Contains(new Point(ball.Right, ball.Bottom)))
                    return true;
                return (brick.Bottom - ball.Top) > (ball.Right - brick.Left);
            }

            //
            if (brick.Contains(new Point(ball.Right, ball.Bottom)))
            {
                return (ball.Bottom - brick.Top) > (ball.Right - brick.Left);
            }

            return false;
        }
    }
}
