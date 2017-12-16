using System.Timers;

namespace Pinball
{
    public class BoundModifier
    {
        private Bound bound;
        private int durationMillis;
        private float modifier;
        private Timer timer;
        private int deltaWidth;


        public BoundModifier(int durationMillis, float modifier)
        {
            bound = GameManager.GetInstance().Bound;
            this.durationMillis = durationMillis;
            this.modifier = modifier;
        }

        public void Start()
        {
            InitTimer();

            int oldWidth = bound.Body.Size.X;
            bound.SetWidth((int)(oldWidth * modifier));
            deltaWidth = bound.Body.Size.X - oldWidth;

            timer.Start();
        }

        private void InitTimer()
        {
            timer = new Timer {Interval = durationMillis};
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            (sender as Timer).Stop();
            bound.SetWidth(bound.Body.Size.X - deltaWidth);
        }
    }
}
