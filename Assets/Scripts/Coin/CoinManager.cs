using _Core;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Coin
{
    public sealed class CoinManager : DisposableSubscriberBase, ITickable
    {
        private readonly IObjectResolver _container;
        private readonly CoinSettings _settings;
        private readonly ISubscriber<CoinDestroyedMessage> _coinDestroyedSubscriber;
        private int _coinCount;
        private float _timer;

        public CoinManager(IObjectResolver container,
            ISubscriber<CoinDestroyedMessage> coinDestroyedSubscriber,
            CoinSettings settings)
        {
            _container = container;
            _coinDestroyedSubscriber = coinDestroyedSubscriber;
            _settings = settings;
            SubscribeToMessages();
        }

        public void Tick()
        {
            if (_coinCount >= _settings.CoinCountLimit) return;

            _timer += Time.deltaTime;

            if (!(_timer >= 2f)) return;

            _timer = 0;
            CreateCoin();
        }

        private void SubscribeToMessages() =>
            _coinDestroyedSubscriber.Subscribe(_ => OnCoinDestroyed()).AddTo(BagBuilder);

        private void OnCoinDestroyed() => _coinCount--;

        private void CreateCoin()
        {
            float x = Random.Range(-800f, 800);
            _container.Instantiate(_settings.CoinPrefab,
                new Vector3(x, 100, 0),
                new Quaternion());
            _coinCount++;
        }
    }
}