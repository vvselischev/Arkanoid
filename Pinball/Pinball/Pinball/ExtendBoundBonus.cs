using System;
using System.Timers;

namespace Pinball
{
    public class ExtendBoundBonus : BonusMethod
    {
        private BoundModifier boundModifier;
        private const float modifier = 1.3f;
        private const int durationMillis = 5000;

        public ExtendBoundBonus()
        {
            Name = BonusTypes.ExtendBoundBonus;
            boundModifier = new BoundModifier(durationMillis, modifier);
        }

        public override void Invoke()
        {
            boundModifier.Start();
        }
    }
}
