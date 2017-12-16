using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public class StartGameInterface : UInterface
    {
        public int SecondToStart
        { get; set; }

        public StartGameInterface(Rectangle screen) : base(screen)
        {
            font = MyContentManager.GetInstance().fonts["InterfaceFont"];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "GET READY:", 
                new Vector2(ScreenWidth * 0.25f, ScreenHeight * 0.25f), Color.Red, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, SecondToStart.ToString(),
                new Vector2(ScreenWidth * 0.45f, ScreenHeight * 0.5f), Color.Gold, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
