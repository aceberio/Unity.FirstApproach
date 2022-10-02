using Character;
using Coin;
using Level;
using MessagePipe;
using Score;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public sealed class GameLifetimeScope : LifetimeScope
    {
        [field: SerializeField] public GameSettings GameSettings { get; private set; }

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMessageBroker(builder);
            RegisterSettings(builder);
            RegisterEntryPoints(builder);
        }

        private static void RegisterMessageBroker(IContainerBuilder builder)
        {
            MessagePipeOptions options = builder.RegisterMessagePipe();

            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

            builder.RegisterMessageBroker<CoinCreatedMessage>(options);
            builder.RegisterMessageBroker<CoinDestroyedMessage>(options);
            builder.RegisterMessageBroker<ScoreChangedMessage>(options);
            builder.RegisterMessageBroker<NewLevelReachedMessage>(options);
        }

        private static void RegisterEntryPoints(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ScoreManager>();
            builder.RegisterEntryPoint<LevelManager>();
            builder.RegisterEntryPoint<UIManager>();
            builder.RegisterEntryPoint<CharacterManager>();
        }

        private void RegisterSettings(IContainerBuilder builder)
        {
            builder.RegisterInstance(GameSettings.CharacterSettings);
            builder.RegisterInstance(GameSettings.CoinSettings);
        }
    }
}