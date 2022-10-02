using _Core;
using Level;
using MessagePipe;
using Score;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace UI
{
    public sealed class UIManager : DisposableSubscriberBase, IInitializable
    {
        private readonly ISubscriber<ScoreChangedMessage> _scoreChangedSubscriber;
        private readonly ISubscriber<NewLevelReachedMessage> _newLevelReachedSubscriber;
        private Text _scoreText;

        public UIManager(ISubscriber<ScoreChangedMessage> scoreChangedSubscriber,
            ISubscriber<NewLevelReachedMessage> newLevelReachedSubscriber)
        {
            _scoreChangedSubscriber = scoreChangedSubscriber;
            _newLevelReachedSubscriber = newLevelReachedSubscriber;
            SubscribeToMessages();
        }

        public void Initialize()
        {
        }

        private void SubscribeToMessages()
        {
            _scoreChangedSubscriber.Subscribe(OnScoreChanged).AddTo(BagBuilder);
            _newLevelReachedSubscriber.Subscribe(OnNewLevelReached).AddTo(BagBuilder);
        }

        private void OnScoreChanged(ScoreChangedMessage scoreChangedMessage) =>
            UpdateScoreInUI(scoreChangedMessage.Score);

        private void OnNewLevelReached(NewLevelReachedMessage newLevelReachedMessage) =>
            UpdateScoreInUI(newLevelReachedMessage.CurrentScore);

        private void UpdateScoreInUI(int score)
        {
            if (_scoreText == null)
                _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            _scoreText.text = $"{score}";
        }
    }
}