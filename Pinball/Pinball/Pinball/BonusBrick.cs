namespace Pinball
{
    public class BonusBrick : Brick
    {
        
        private FallingBonus bonus;
        public BonusBrick(Body body, FallingBonus bonus, int score) : base(body, score)
        {
            this.bonus = bonus;
        }


        public override void Action()
        {
            base.Action();
            bonus.Activate();
        }

    }
}
