using UnityEngine;
using UnityEngine.UI;

public sealed class ScoreHandler : MonoBehaviour
{
    private static ScoreHandler _instance;

    private ScoreHandler()
    {
    }

    public static ScoreHandler Instance
    {
        get
        {
            _instance = SingletonResolver.Resolve(_instance);
            return _instance;
        }
    }

    private int _score { get; set; }
    private Text _scoreText { get; set; }

    public void IncreaseScore()
    {
        _score++;
        UpdateScoreInUI();
        if (_score != 5 || SceneHandler.Instance.GetActiveScene().name == "Scene02") return;
        SceneHandler.Instance.LoadScene("Scene02", UpdateScoreInUI);
    }

    private void UpdateScoreInUI()
    {
        if (_scoreText == null)
            _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        _scoreText.text = $"{_score}";
    }
}