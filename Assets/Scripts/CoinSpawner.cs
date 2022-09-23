using UnityEngine;

public sealed class CoinSpawner : MonoBehaviour
{
    private static CoinSpawner _instance;
    private int _coinCount;
    private float _timer;

    private CoinSpawner()
    {
    }

    public static CoinSpawner Instance => _instance;

    [field: SerializeField] public GameObject Seed { get; set; }
    [field: SerializeField] public int CoinCountLimit { get; set; } = 10;

    public void DestroyCoin(GameObject coin)
    {
        Destroy(coin);
        _coinCount--;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    private void Update()
    {
        if (_coinCount >= CoinCountLimit) return;

        _timer += Time.deltaTime;

        if (!(_timer >= 2f)) return;

        _timer = 0;
        CreateCoin();
    }

    private void CreateCoin()
    {
        float x = Random.Range(-800f, 800);
        Instantiate(Seed, new Vector3(x, 100, 0), new Quaternion());
        _coinCount++;
    }
}