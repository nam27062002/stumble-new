using System;

namespace Extensions.Reactive.Runtime
{
    public interface IDisposer
    {
        void Add(IDisposable disposable);
    }
}