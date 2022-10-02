using Coin;
using VContainer;
using VContainer.Unity;

namespace Level
{
    public sealed class LevelLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder) => RegisterEntryPoints(builder);

        private static void RegisterEntryPoints(IContainerBuilder builder) => builder.RegisterEntryPoint<CoinManager>();
    }
}