using MessagePipe;
using System;

namespace _Core
{
    public abstract class DisposableSubscriberBase : IDisposable
    {
        protected DisposableBagBuilder BagBuilder { get; } = DisposableBag.CreateBuilder();

        public void Dispose()
        {
            BagBuilder.Build().Dispose();
        }
    }
}