using System;

namespace Pinball
{
    public class LifeManager
    {
        private static LifeManager instance;
        public static LifeManager GetInstance()
        {
            return instance ?? (instance = new LifeManager());
        }

        private LifeManager()
        {
        }

        public void Reset()
        {
            lives = 0;
        }

        public event VoidHandler OnLivesChanged;

        private int lives;
        public int Lives
        {
            get
            {
                return lives;
            }
            set
            {
                if (value < 0)
                    throw new Exception("Попытка задать отрицательное кол-во жизней");
                lives = value;

                OnLivesChanged?.Invoke();
            }
        }
    }
}
