using Microsoft.Xna.Framework;

namespace Pinball
{
    public class Transform
    {
        public Vector2 velocity;
        public Vector2 position;
        private Vector2 originalVelocity;
        private float scale;
        private float originalScale;

        public Vector2 OriginalVelocity => originalVelocity;

        public float Scale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value;
            }
        }

        public float OriginalScale => originalScale;

        public Transform(Vector2 position, Vector2 originalVelocity, float originalScale = 1)
        {
            this.position = position;
            scale = originalScale;
            this.originalVelocity = originalVelocity;
            velocity = originalVelocity;
            this.originalScale = originalScale;
        }

        public float speed => velocity.Length();

        public void ModifyScale(float modifier)
        {
            scale *= modifier;
        }

        public void ResetScale()
        {
            scale = originalScale;
        }

        public void ModifyVelocity(float modifier)
        {
            velocity *= modifier;
        }

        public Transform Clone()
        {
            return new Transform(position, velocity, scale);
        }

        public void ResetVelocity()
        {
            velocity = originalVelocity;
        }
    }
}
