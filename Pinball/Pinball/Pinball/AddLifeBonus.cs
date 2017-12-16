namespace Pinball
{
    public class AddLifeBonus : BonusMethod
    {
        public AddLifeBonus()
        {
            Name = BonusTypes.AddLifeBonus;
            BonusBrickFactory.GetInstance().AvaliableBonuses[BonusTypes.AddLifeBonus] = false;
        }

        public override void Invoke()
        {
            GameManager.GetInstance().Lives++;
        }
    }
}