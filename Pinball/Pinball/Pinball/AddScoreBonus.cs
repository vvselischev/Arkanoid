namespace Pinball
{
    public class AddScoreBonus : BonusMethod
    {
        private const int bonusScore = 100;

        public AddScoreBonus()
        {
            Name = BonusTypes.AddScoreBonus;
        }

        public override void Invoke()
        {
            ScoreManager.GetInstance().AddScore(bonusScore);
        }
    }
}
