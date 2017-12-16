using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pinball
{
    public class BonusManager
    {
        private List<Bonus> activeBonuses;

        private BonusManager()
        {
            activeBonuses = new List<Bonus>();            
        }

        private static BonusManager instance;
        private bool bonusCountChanged;

        public static BonusManager GetInstance()
        {
            return instance ?? (instance = new BonusManager());
        }

        public List<Bonus> ActiveBonuses
        {
            get
            {
                return activeBonuses;
            }

            set
            {
                activeBonuses = value;
            }
        }

        public void AddBonus(Bonus bonus)
        {
            ActiveBonuses.Add(bonus);
        } 

        public void Update(GameTime gameTime)
        {
            foreach (FallingBonus bonus in ActiveBonuses)
            {
                GameManager.GetInstance().CheckBonusCollider(bonus);

                if (bonusCountChanged)
                {
                    bonusCountChanged = false;

                    //Обновляем оставшиеся.
                    Update(gameTime);
                    break;
                }

                bonus.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (FallingBonus bonus in ActiveBonuses)
                bonus.Draw(sb);
        }

        public void RemoveBonus(FallingBonus bonus)
        {
            activeBonuses.Remove(bonus);
            bonus = null;
            bonusCountChanged = true;
        }

        public void ResetBonuses()
        {
            activeBonuses.Clear();
        }
    }
}
