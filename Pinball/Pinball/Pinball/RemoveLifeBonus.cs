using System;

namespace Pinball
{
    public class RemoveLifeBonus : BonusMethod
    {
        public RemoveLifeBonus()
        {
            Name = BonusTypes.RemoveLifeBonus;
        }

        public override void Invoke()
        {
             GameManager.GetInstance().Lives--;
        }
    }
}
