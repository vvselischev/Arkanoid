using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pinball
{
    public class Bound 
    {
        private Body body;
        private KeyboardState keyboardState;
        private Rectangle clientWindow;
        private Point defaultSize;

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

        public Bound(Point size, Transform transform, Rectangle clientWindow)
        {
            body = new Body(transform, size);
            defaultSize = size;
            this.clientWindow = clientWindow;
        }

        public void SetDefaultWidth()
        {
            SetWidth(defaultSize.X);
        }

        public void SetWidth(int newWidth)
        {
            body.Size = new Point(newWidth, body.Size.Y);

            if (body.Size.X > clientWindow.Width)
                body.Size = new Point(clientWindow.Width - 2, body.Size.Y);

            CheckOutOfScreen();
        }

        private void CheckOutOfScreen()
        {
            if (body.Transform.position.X < 0)
                body.Transform.position.X = 0;
            if (body.Transform.position.X + body.Size.X > clientWindow.Width)
                body.Transform.position.X = clientWindow.Width - body.Size.X;
        }

        public void Update(GameTime gameTime)
        {
            Transform transform = body.Transform;

            keyboardState = Keyboard.GetState();
           
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Vector2 oldPosition = new Vector2(transform.position.X, transform.position.Y);
                transform.position.X -= transform.speed * gameTime.ElapsedGameTime.Milliseconds;
                if (body.OutOfScreen(clientWindow))
                    transform.position = oldPosition;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Vector2 oldPosition = new Vector2(transform.position.X, transform.position.Y);
                transform.position.X += transform.speed * gameTime.ElapsedGameTime.Milliseconds;
                if (body.OutOfScreen(clientWindow))
                    transform.position = oldPosition;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            body.Draw(spriteBatch);
        }
    }
}
