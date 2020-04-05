using System;


namespace Sellswords
{
    public interface IInvokable
    {
        void Invoke(Action method, float delayTime);
        void InvokeRepeating(Action method, float time, float tick, Action callback);
    }
}