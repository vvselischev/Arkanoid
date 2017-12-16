using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public abstract class AbstractBrickFactory
    {
        protected MyContentManager content;

        public Texture2D Texture { get; set; }

        public abstract Brick Create(System.Drawing.Color color, Transform transform, Point size);
    }
}
