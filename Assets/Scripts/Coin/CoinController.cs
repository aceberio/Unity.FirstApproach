using MessagePipe;
using UnityEngine;
using VContainer;

namespace Coin
{
    public sealed class CoinController : MonoBehaviour
    {
        private IPublisher<CoinDestroyedMessage> _coinDestroyedPublisher;

        [Inject]
        public void Construct(IPublisher<CoinDestroyedMessage> coinDestroyedPublisher) =>
            _coinDestroyedPublisher = coinDestroyedPublisher;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(gameObject);
            _coinDestroyedPublisher.Publish(new CoinDestroyedMessage());
        }
    }
}