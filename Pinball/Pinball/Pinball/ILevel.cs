using Microsoft.Xna.Framework;

namespace Pinball
{
    public interface ILevel
    {
        void Start();
        void Update(GameTime gameTime);
    }
}
