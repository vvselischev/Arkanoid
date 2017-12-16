using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pinball
{
    public class MyContentManager
    {
        private static MyContentManager instance;

        public static MyContentManager GetInstance()
        {
            return instance ?? (instance = new MyContentManager());
        }

        private MyContentManager()
        {
            maps = new List<System.Drawing.Bitmap>();
            textures = new Dictionary<string, Texture2D>();
            bonusTextures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
        }

        public void Initialize(ContentManager content)
        {
            //maps.Add(new System.Drawing.Bitmap(System.Drawing.Image.FromFile("Content\\MapTest.bmp")));
            maps.Add(new System.Drawing.Bitmap(System.Drawing.Image.FromFile("Content\\Map1.bmp")));
            maps.Add(new System.Drawing.Bitmap(System.Drawing.Image.FromFile("Content\\MapRainbow.bmp")));
            

            textures.Add("BallTexture", content.Load<Texture2D>("Textures\\MyBall"));
            textures.Add("SimpleBrickTexture", content.Load<Texture2D>("Textures\\Brick"));
            textures.Add("BonusBrickTexture", content.Load<Texture2D>("Textures\\Brick"));
            textures.Add("BoundTexture", content.Load<Texture2D>("Textures\\Platform1"));
            textures.Add("BackTexture", content.Load<Texture2D>("Textures\\BackTexture"));

            bonusTextures.Add(BonusTypes.AddBallBonus.ToString(), content.Load<Texture2D>("Textures\\AddBallBonus"));
            bonusTextures.Add(BonusTypes.ExtendBoundBonus.ToString(), content.Load<Texture2D>("Textures\\ExtendBoundBonus"));
            bonusTextures.Add(BonusTypes.RemoveLifeBonus.ToString(), content.Load<Texture2D>("Textures\\RemoveLifeBonus"));
            bonusTextures.Add(BonusTypes.AddLifeBonus.ToString(), content.Load<Texture2D>("Textures\\LifeTexture"));
            bonusTextures.Add(BonusTypes.AddScoreBonus.ToString(), content.Load<Texture2D>("Textures\\AddScoreBonus"));
            bonusTextures.Add(BonusTypes.ReduceBoundBonus.ToString(), content.Load<Texture2D>("Textures\\ReduceBoundBonus"));

            fonts.Add("InterfaceFont", content.Load<SpriteFont>("MyFont"));
        }

        public List<System.Drawing.Bitmap> maps;
        public Dictionary<string, Texture2D> textures;
        public Dictionary<string, SpriteFont> fonts;
        public Dictionary<string, Texture2D> bonusTextures;
    }
}
