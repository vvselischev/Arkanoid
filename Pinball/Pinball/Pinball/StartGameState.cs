using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace Pinball
{
    public class StartGameState : GameState
    {
        StartGameInterface gui;
        private Timer timer;
        private int secondsToStart;
        public StartGameState(int secondsToStart)
        {
            stateManager = StateManager.GetInstance();
            gui = new StartGameInterface(GameManager.GetInstance().ScreenRect);
            this.secondsToStart = secondsToStart;
            gui.SecondToStart = this.secondsToStart;
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            secondsToStart--;
            gui.SecondToStart = secondsToStart;
            if (secondsToStart == 0)
            {
                timer.Stop();
                stateManager.CurrentState = new PlayState(3);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            gui.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
