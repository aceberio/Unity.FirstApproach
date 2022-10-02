using _Core;
using Coin;
using MessagePipe;
using VContainer.Unity;

namespace Score
{
    public sealed class ScoreManager : DisposableSubscriberBase, IInitializable
    {
        private readonly ISubscriber<CoinDestroyedMessage> _coinDestroyedSubscriber;

        private readonly IPublisher<ScoreChangedMessage> _scoreChangedPublisher;
        private int _score;

        public ScoreManager(ISubscriber<CoinDestroyedMessage> coinDestroyedSubscriber,
            IPublisher<ScoreChangedMessage> scoreChangedPublisher)
        {
            _coinDestroyedSubscriber = coinDestroyedSubscriber;
            _scoreChangedPublisher = scoreChangedPublisher;
            SubscribeToMessages();
        }

        public void Initialize()
        {
        }

        private void SubscribeToMessages() =>
            _coinDestroyedSubscriber.Subscribe(_ => OnCoinDestroyed()).AddTo(BagBuilder);

        private void OnCoinDestroyed()
        {
            _score++;
            _scoreChangedPublisher.Publish(new ScoreChangedMessage { Score = _score });
        }
    }
}