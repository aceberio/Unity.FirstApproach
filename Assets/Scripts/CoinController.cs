using UnityEngine;

public sealed class CoinController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinManager.Instance.DestroyCoin(gameObject);
        ScoreManager.Instance.IncreaseScore();
    }
}
