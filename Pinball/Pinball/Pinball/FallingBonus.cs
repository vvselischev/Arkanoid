using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public class FallingBonus : Bonus
    {
        private Body body;
        private Texture2D texture;

        public FallingBonus(Texture2D texture, Body body, BonusMethod strategy) : base(strategy)
        {
            Texture = texture;
            BonusManager.GetInstance();
            this.body = body;
        }

        public Body Body => body;

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

        public override void Activate()
        {
            base.Activate();
        }

        public void Invoke()
        {
            Strategy.Invoke();
        }

        public void Update(GameTime gameTime)
        {
            body.Transform.position += body.Transform.velocity * gameTime.ElapsedGameTime.Milliseconds;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
                sb.Draw(texture, body.Collider, Color.White);
            sb.End();
        }
    }
}