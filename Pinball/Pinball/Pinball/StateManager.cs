namespace Pinball
{
    public class StateManager
    {
        private GameState currentState;
        private Game1 game;

        private static StateManager instance;

        public static StateManager GetInstance()
        {
            return instance ?? (instance = new StateManager());
        }

        public void Init(Game1 game)
        {
            this.game = game;
        }

        private StateManager()
        {
        }

        public GameState CurrentState
        {
            get
            {
                return currentState;
            }

            set
            {
                currentState = value;
                if (currentState is GameOverState)
                    (currentState as GameOverState).gameToOver = game;
            }
        }
    }
}
