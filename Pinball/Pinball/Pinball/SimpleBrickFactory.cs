using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public class SimpleBrickFactory : AbstractBrickFactory
    {
        private const int simpleScore = 10;

        private static SimpleBrickFactory instance;

        public static SimpleBrickFactory GetInstance()
        {
            return instance ?? (instance = new SimpleBrickFactory());
        }

        private SimpleBrickFactory()
        {
            
        }

        public void Initialize(Texture2D texture)
        {
            Texture = texture;
        }

        public override Brick Create(System.Drawing.Color color, Transform transform, Point size)
        {
            Body body = new Body(color, transform, size);
            body.Texture = Texture;
            Brick brick = new SimpleBrick(body, simpleScore);    
            return brick;
        }
    }
}
