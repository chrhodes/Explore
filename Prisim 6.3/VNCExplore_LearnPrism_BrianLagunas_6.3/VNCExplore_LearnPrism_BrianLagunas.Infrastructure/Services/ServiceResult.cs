using System;

namespace VNCExplore_LearnPrism_BrianLagunas.Infrastructure
{
    public class ServiceResult<T> : EventArgs
    {
        public T Object { get; private set; }

        public ServiceResult(T obj)
        {
            Object = obj;
        }
    }
}
