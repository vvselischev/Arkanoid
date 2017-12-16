using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pinball
{

    public delegate void VoidHandler();
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D backTexture;
        //GameTime gameTime;

        //SpriteFont font;

        Ball ball;
        //Board<Brick> board;
        Bound bound;

        //bool started;

        LevelManager<PinballLevel> levelManager;
        MyContentManager contentManager;
        BallManager ballManager;
        GameManager gameManager;
        //PlayInterface ui;

        StateManager stateManager;

        int screenWidth;
        int screenHeight;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //IsMouseVisible = true;
            //graphics.IsFullScreen = true;

            graphics.ToggleFullScreen();
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;

            //graphics.PreferredBackBufferHeight = 600;
            //graphics.PreferredBackBufferWidth = 1000;
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();


            screenHeight = graphics.PreferredBackBufferHeight;
            screenWidth = graphics.PreferredBackBufferWidth;

            
            levelManager = LevelManager<PinballLevel>.GetInstance();
            gameManager = GameManager.GetInstance();

            int width = 7;
            int height = 20;
            bound = new Bound(
                size: new Point(graphics.PreferredBackBufferWidth/width, graphics.PreferredBackBufferHeight/height),
                transform: new Transform(new Vector2(graphics.PreferredBackBufferWidth*(width - 1)/(width*2),
                    graphics.PreferredBackBufferHeight*(height - 1)/height),
                    new Vector2(0.4f*graphics.PreferredBackBufferHeight/600, 0)),
                clientWindow: new Rectangle(0, 0, screenWidth, screenHeight))
            {
                Body = {Texture = contentManager.textures["BoundTexture"]}
            };

            gameManager.Init(levelManager, bound, new Rectangle(0, 0, screenWidth, screenHeight));
            //levelManager.Initialize();

            SimpleBrickFactory sbf = SimpleBrickFactory.GetInstance();
            sbf.Initialize(contentManager.textures["SimpleBrickTexture"]);
            BonusBrickFactory bbf = BonusBrickFactory.GetInstance();
            bbf.Initialize(contentManager.textures["SimpleBrickTexture"], contentManager.bonusTextures, 
                fallingVelocity: 0.3f * graphics.PreferredBackBufferHeight / 500,
                size: new Point(20 * screenHeight / 500, 20 * screenHeight / 500));
            bbf.DefaultBonusMethod = BonusTypes.AddScoreBonus;


            
            

            

            ball = new Ball(contentManager.textures["BallTexture"],
                frameSize: new Point(300, 300),
                sheetSize: new Point(6, 5),
                frameCollision: 75,
                frameCount: 29,
                position: new Vector2(),
                velocity: new Vector2(0.25f * graphics.PreferredBackBufferHeight / 600, -0.25f * graphics.PreferredBackBufferHeight / 600),
                millisecondsPerFrame: 30);
            ball.Body.Transform.ModifyScale(0.1f * graphics.PreferredBackBufferHeight / 500);



            //ui = PlayInterface.GetInstance();


            //ui.ScreenWindow = new Rectangle(0, 0, screenWidth, screenHeight);
            //ui.LifeBall = ball.Clone();

            ballManager = BallManager.GetInstance();
            ballManager.Init(ball.Clone());
            ballManager.GameBallPrototype = ball.Clone();

            //ui.LifeBall = ballManager.InterfaceBallPrototype;
            //ui.LifeBall = ball.Sprite;

            PinballLevelCreator levelCreator = PinballLevelCreator.GetInstance();
            levelCreator.Initialize(
                boardSize: new Point(20, 20),
                offset: new Point(50 * screenHeight / 500, 50 * screenHeight / 500),
                brickSize: new Point((screenWidth - 2 * 50 * screenHeight / 500) / 20, (screenHeight - 50 * screenHeight / 500 - 200 * screenHeight / 500) / 20),
                bound: bound,
                bonusPossibility: 30);

           
            
            //gameManager.ScreenRect = 
            levelManager.AddLevels(levelCreator.CreateFromBitmapList(contentManager.maps));

            stateManager = StateManager.GetInstance();
            stateManager.Init(this);
            StartGame();
        }

        private void StartGame()
        {
            stateManager.CurrentState = new StartGameState(5);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            contentManager = MyContentManager.GetInstance();
            contentManager.Initialize(Content);
            backTexture = contentManager.textures["BackTexture"];

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {               

            // Allows the game to exit
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || (Keyboard.GetState().IsKeyDown(Keys.Escape)))
                Exit();


            // TODO: Add your update logic here
            //screenWidth = graphics.PreferredBackBufferWidth;
            //screenHeight = graphics.PreferredBackBufferHeight;
            //Rectangle clientWindow = new Rectangle(0, 0, screenWidth, screenHeight);


            //gameManager.UpdateAllManagers(gameTime, clientWindow);
            stateManager.CurrentState.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(backTexture, new Rectangle(0, 0, screenWidth, screenHeight), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);

            spriteBatch.End();
            stateManager.CurrentState.Draw(spriteBatch);
            //gameManager.DrawAllManagers(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
