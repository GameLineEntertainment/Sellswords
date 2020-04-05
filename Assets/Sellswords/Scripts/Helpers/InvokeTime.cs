using System;


namespace Sellswords
{
    public class InvokeTime : Invoke
    {
        #region Fields

        public float StartTime;
        public float Interval;
        public float Timer;
        public float Time;
        public Action Callback;

        #endregion


        #region ClassLifeCycles

        public InvokeTime(Action method, float startTime, float time, float interval, Action callback = null) : base(method)
        {
            StartTime = startTime;
            Interval = interval;
            Time = time;
            Callback = callback;
        }

        #endregion
    }
}