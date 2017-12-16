namespace Pinball
{
    public abstract class Bonus
    {
        private BonusMethod strategy;
        //private BonusManager manager;

        public Bonus(BonusMethod strategy)
        {
            //this.manager = BonusManager.GetInstance();
            this.strategy = strategy;
        }

        public virtual void Activate()
        {
            //manager.AddBonus(this);
            GameManager.GetInstance().ActivateBonus(this);
        }

        public BonusMethod Strategy
        {
            get
            {
                return strategy;
            }

            set
            {
                strategy = value;
            }
        }

    }
}
