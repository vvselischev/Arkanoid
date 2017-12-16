namespace Pinball
{
    public class AddBallBonus : BonusMethod
    {
        public AddBallBonus()
        {
            Name = BonusTypes.AddBallBonus;
        }

        public override void Invoke()
        {
            GameManager.GetInstance().AddBallToCenterBound();
        }
    }
}
