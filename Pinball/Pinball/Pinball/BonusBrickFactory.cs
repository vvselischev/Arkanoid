using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public enum BonusTypes
    {
        ReduceBoundBonus,
        AddLifeBonus,
        AddBallBonus,
        ExtendBoundBonus,
        RemoveLifeBonus,
        AddScoreBonus,
    }

    public class BonusBrickFactory : AbstractBrickFactory
    {
        private Random rnd;
        private float fallingVelocity;
        private Point bonusSize;
        private Dictionary<string, Texture2D> textures;

        private const int bonusScore = 20;

        private BonusTypes defaultBonusMethodType;

        private static BonusBrickFactory instance;
        public static BonusBrickFactory GetInstance()
        {
            return instance ?? (instance = new BonusBrickFactory());
        }

        private Dictionary<BonusTypes, bool> avaliableBonuses;

        public Dictionary<BonusTypes, bool> AvaliableBonuses
        {
            get
            {
                return avaliableBonuses;
            }

            set
            {
                avaliableBonuses = value;
            }
        }

        public BonusTypes DefaultBonusMethod
        {
            get
            {
                return defaultBonusMethodType;
            }

            set
            {
                defaultBonusMethodType = value;
            }
        }

        private void InitAvailableBonuses()
        {
            avaliableBonuses = new Dictionary<BonusTypes, bool>
            {
                {BonusTypes.AddBallBonus, true},
                {BonusTypes.AddLifeBonus, true},
                {BonusTypes.ExtendBoundBonus, true},
                {BonusTypes.RemoveLifeBonus, true},
                {BonusTypes.AddScoreBonus, true},
                {BonusTypes.ReduceBoundBonus, true}
            };
        }

        public void SetAllBonusesAvailable()
        {
            InitAvailableBonuses();
        }

        public void Initialize(Texture2D brickTexture, Dictionary<string, Texture2D> fallingTextures, float fallingVelocity, Point size)
        {
            Texture = brickTexture;
            textures = fallingTextures;
            this.fallingVelocity = fallingVelocity;
            bonusSize = size;
            InitAvailableBonuses();
        }

        private BonusBrickFactory()
        {    
            rnd = new Random();
        }

        public override Brick Create(System.Drawing.Color color, Transform transform, Point size)
        {
            Body newBody = new Body(color, transform, size) {Texture = Texture};
            Brick brick = new BonusBrick(newBody, CreateFallingBonus(transform.position, bonusSize, GetRandomMethod()), bonusScore);    
            return brick;
        }

        private FallingBonus CreateFallingBonus(Vector2 position, Point size, BonusMethod bonusMethod)
        {
            Body fallingBody = new Body(new Transform(position, new Vector2(0, fallingVelocity)), size);           
            Texture2D bonusTexture = textures[bonusMethod.Name.ToString()];
            FallingBonus bonus = new FallingBonus(bonusTexture, fallingBody, bonusMethod);
            return bonus;
        }

        private BonusMethod GetRandomMethod()
        {
            int psb = rnd.Next(AvaliableBonuses.Count - 1);

            BonusTypes currentType = (BonusTypes)psb;
            return CheckAvailabble(currentType);

        }

        private BonusMethod CheckAvailabble(BonusTypes type)
        {
            if (AvaliableBonuses[type])
            {
                return GetMethod(type);
            }
            return GetMethod(defaultBonusMethodType);
        }

        private BonusMethod GetMethod(BonusTypes type)
        {
            string typeString = GetType().Namespace + "." + type.ToString();
            dynamic product = Activator.CreateInstance(Type.GetType(typeString));
            return product as BonusMethod;
        }
    }
}
