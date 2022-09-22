using UnityEngine;

public sealed class CoinManager : MonoBehaviour
{
    private static int _coinCount;
    private float _timer;

    public static CoinManager Instance { get; private set; }

    [field: SerializeField] public GameObject Seed { get; set; }
    [field: SerializeField] public int CoinCountLimit { get; set; } = 10;

    public void DestroyCoin(GameObject coin)
    {
        Destroy(coin);
        _coinCount--;
    }

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(this);
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