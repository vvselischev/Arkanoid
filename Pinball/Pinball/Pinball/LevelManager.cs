using System;
using System.Collections.Generic;

namespace Pinball
{
    public class LevelManager<T> where T : ILevel
    {
        private int currentLevelNumber;
        private static LevelManager<T> instance;

        public static LevelManager<T> GetInstance()
        {
            return instance ?? (instance = new LevelManager<T>());
        }

        private LevelManager()
        {
            Initialize();
        }

        private List<T> levels;

        public int LevelCount
        {
            get
            {
                return levels.Count;
            }
            set
            {
                LevelCount = value;
            }
        }


        public T CurrentLevel => levels[InternalCurrentLevelNumber];

        public int CurrentLevelNumber => currentLevelNumber;

        private int InternalCurrentLevelNumber
        {
            get
            {
                return currentLevelNumber - 1;
            }

            set
            {
                currentLevelNumber = value;
            }
        }

        public List<T> Levels
        {
            get
            {
                return levels;
            }
        }

        public void AddLevels(List<T> levels)
        {
            foreach (T level in levels)
                AddLevel(level);
        }

        public virtual void Initialize()
        {
            if (levels == null)
            {
                levels = new List<T>();
            }
       }

        public virtual void SetLevel(int number)
        {
            if ((number < 1) || (number > LevelCount))
                throw new ArgumentException("Попытка обратиться к несуществующему уровню");

            InternalCurrentLevelNumber = number;
            levels[InternalCurrentLevelNumber].Start();
        }

        public virtual void MoveNext()
        {
            SetLevel(currentLevelNumber + 1);
        }

        public virtual void AddLevel(T level)
        {
            levels.Add(level);
        }
    }
}
