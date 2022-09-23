using Factories;
using UnityEngine;
using UnityEngine.UI;

public sealed class ScoreHandler : MonoBehaviour
{
    private static ScoreHandler _instance;

    private ScoreHandler()
    {
    }

    public static ScoreHandler Instance =>
        _instance ??= MonoBehaviourFactory.Create<ScoreHandler>(ObjectScopeType.Game);

    private int _score { get; set; }
    private Text _scoreText { get; set; }

    public void IncreaseScore()
    {
        _score++;
        UpdateScoreInUI();
        if (_score != 5 || SceneHandler.Instance.GetActiveScene().name == "Scene02") return;
        SceneHandler.Instance.LoadScene("Scene02", UpdateScoreInUI);
    }

    private void Awake()
    {
        if (_instance == null || _instance == this) return;
        Destroy(this);
    }

    private void UpdateScoreInUI()
    {
        if (_scoreText == null)
            _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        _scoreText.text = $"{_score}";
    }
}