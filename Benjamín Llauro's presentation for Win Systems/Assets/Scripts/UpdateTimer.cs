namespace Tools
{
    public class UpdateTimer
    {
        enum State
        {
            None,
            Running,
            Finished
        }
        private float timer = 0.0f;
        private float accum = 0.0f;
        private State state = State.None;

        public void Start()
        {
            accum = 0.0f;
            state = State.Running;
        }
        public void StopAndReset()
        {
            accum = 0.0f;
            state = State.None;
        }
        public void SetTimer(float time)
        {
            timer = time;
            accum = 0.0f;
        }
        public bool GetStarted()
        {
            if (state == State.Running)
                return true;
            else
                return false;
        }
        public bool GetFinished()
        {
            if (state == State.Finished)
                return true;
            else
                return false;
        }
        public bool Update(float dt)
        {
            if (state == State.Running)
            {
                accum += dt;
                if (accum >= timer)
                {
                    state = State.Finished;
                    return true;
                }
            }
            else if (state == State.Finished)
                return true;

            return false;
        }
    }
}