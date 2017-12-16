namespace Pinball
{
    public class ReduceBoundBonus : BonusMethod
    {
        private BoundModifier boundModifier;
        private const float modifier = 0.7f;
        private const int durationMillis = 5000;

        public ReduceBoundBonus()
        {
            Name = BonusTypes.ReduceBoundBonus;
            boundModifier = new BoundModifier(durationMillis, modifier);
        }

        public override void Invoke()
        {
            boundModifier.Start();
        }
    }
}
