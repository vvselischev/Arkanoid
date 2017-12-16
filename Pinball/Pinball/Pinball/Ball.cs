using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public class Ball
    {
        private Sprite sprite;
        private Body body;

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

        public Sprite Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        public Ball(Texture2D textureImage, Point frameSize, Point sheetSize, int frameCollision, int frameCount,
            Vector2 position, Vector2 velocity, int millisecondsPerFrame, string collisionCueName = "") 
            
        {
            body = new Body(System.Drawing.Color.White, new Transform(position, velocity), frameSize, frameCollision);
            sprite = new Sprite(textureImage, frameSize, sheetSize, frameCollision, frameCount,
                    body.Transform, millisecondsPerFrame, collisionCueName); 
        }

        public Ball(Sprite sprite)
        {
            this.sprite = sprite;
            body = new Body(System.Drawing.Color.White, sprite.Transform, sprite.FrameSize, sprite.FrameCollision);
        }

        public bool CheckOutOfScreen(Rectangle clientWindow)
        {
            if ((body.Collider.X + body.Collider.Width >= clientWindow.Width) || (body.Collider.X <= 0))
                body.Transform.velocity.X = -body.Transform.velocity.X;

            if (body.Collider.Y + body.Collider.Height >= clientWindow.Height)
            {
                body.Transform.velocity.Y = -body.Transform.velocity.Y;
                return true;
            }

            if (body.Collider.Y < 0)
                body.Transform.velocity.Y = -body.Transform.velocity.Y;

            return false;
        }

        public void Update(GameTime gameTime)
        {
            body.Transform.position += body.Transform.velocity * gameTime.ElapsedGameTime.Milliseconds;

            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        public Ball Clone()
        {
            return new Ball(sprite.Clone());
        }
    }
}
