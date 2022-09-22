using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [field: SerializeField] public Text Score { get; set; }
    private static int _score { get; set; }

    public void IncreaseScore()
    {
        _score++;
        Score.text = $"{_score}";
        if (_score == 5 && SceneManager.GetActiveScene().name != "Scene02") SceneManager.LoadScene("Scene02");
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Score = GameObject.Find("Score").GetComponent<Text>();
            Score.text = $"{_score}";
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }
}