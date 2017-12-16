namespace Pinball
{
    public abstract class BonusMethod
    {
        public BonusTypes Name
        {
            get;
            protected set;
        }

        public abstract void Invoke();
    }
}
