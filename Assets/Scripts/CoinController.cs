using UnityEngine;

public sealed class CoinController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinSpawner.Instance.DestroyCoin(gameObject);
        ScoreHandler.Instance.IncreaseScore();
    }
}
