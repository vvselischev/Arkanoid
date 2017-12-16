using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public abstract class UInterface
    {
        protected SpriteFont font;
        private int screenWidth;
        private int screenHeight;

        public Rectangle ScreenWindow
        {
            get
            {
                return new Rectangle(0, 0, ScreenWidth, ScreenHeight);
            }
            set
            {
                screenWidth = value.Width;
                screenHeight = value.Height;
            }
        }

        public SpriteFont Font
        {
            get
            {
                return font;
            }

            set
            {
                font = value;
            }
        }

        public int ScreenWidth => screenWidth;

        public int ScreenHeight => screenHeight;

        public UInterface(Rectangle screen)
        {
            ScreenWindow = screen;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
