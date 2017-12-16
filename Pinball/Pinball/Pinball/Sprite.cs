using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public class Sprite
    {
        Texture2D textureImage;
        Point currentFrame;
        int frameCount;
        int framesLeft = 1;
        Point sheetSize;

        private Point frameSize;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        int frameCollision;
        const int defaultMillisecondsPerFrame = 16;
        Transform transform;

        public Rectangle collisionRect
        {
            get
            {
                Vector2 position = transform.position;
                float scale = transform.Scale;
                return new Rectangle((int)(position.X + frameCollision * scale), (int)(position.Y + frameCollision * scale),
                    (int)((frameSize.X - frameCollision * 2) * scale), (int)((frameSize.Y - frameCollision * 2) * scale));
            }
        }

        public string collisionCueName
        {
            get;
            set;
        }

        public Transform Transform => transform;

        public Point FrameSize
        {
            get
            {
                return frameSize;
            }

            set
            {
                frameSize = value;
            }
        }

        public int FrameCollision
        {
            get
            {
                return frameCollision;
            }

            set
            {
                frameCollision = value;
            }
        }

        public Sprite Clone()
        {
            Sprite newSprite = new Sprite(textureImage, frameSize, sheetSize, frameCollision, frameCount, transform.Clone(), millisecondsPerFrame);
            return newSprite;
        }


       public Sprite(Texture2D textureImage, Point frameSize, Point sheetSize, int frameCollision, int frameCount,
            Transform transform, int millisecondsPerFrame, string collisionCueName = "")
        {
            this.textureImage = textureImage;
            this.currentFrame = new Point(0, 0);
            this.frameCount = frameCount;
            this.sheetSize = sheetSize;
            this.frameSize = frameSize;
            this.millisecondsPerFrame = millisecondsPerFrame;

            this.transform = transform;
            this.frameCollision = frameCollision;
            this.collisionCueName = collisionCueName;
        }
     

        /*public bool OutOfScreen(Rectangle clientWindow)
        {
            Vector2 position = transform.position;
            if ((position.X < -frameSize.X) || (position.X > clientWindow.Width) ||
                (position.Y < -frameSize.Y) || (position.Y > clientWindow.Height))
                return true;
            else
                return false;
        }*/

        public virtual void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                ++framesLeft;
                if (framesLeft > frameCount)
                {
                    framesLeft = 1;
                    currentFrame.X = 0;
                    currentFrame.Y = 0;
                }
                else
                {
                    ++currentFrame.X;
                    if (currentFrame.X >= sheetSize.X)
                    {
                        currentFrame.X = 0;
                        ++currentFrame.Y;
                        if (currentFrame.Y >= sheetSize.Y)
                            currentFrame.Y = 0;
                    }
                }
            }

        }

        public Point Size
        {
            get
            {
                return new Point((int)(frameSize.X * transform.Scale), (int)(frameSize.Y * transform.Scale));
            }
            set
            {

            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = transform.position;
            float scale = transform.Scale;
            spriteBatch.Begin();
                spriteBatch.Draw(textureImage, position, new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    
    }
}
