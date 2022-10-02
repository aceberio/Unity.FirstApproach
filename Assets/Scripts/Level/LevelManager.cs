using _Core;
using Cysharp.Threading.Tasks;
using MessagePipe;
using Score;
using System;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Level
{
    public sealed class LevelManager : DisposableSubscriberBase, IInitializable
    {
        private readonly ISubscriber<ScoreChangedMessage> _scoreChangedSubscriber;
        private readonly IPublisher<NewLevelReachedMessage> _newLevelReachedPublisher;

        public LevelManager(ISubscriber<ScoreChangedMessage> scoreChangedSubscriber,
            IPublisher<NewLevelReachedMessage> newLevelReachedPublisher)
        {
            _scoreChangedSubscriber = scoreChangedSubscriber;
            _newLevelReachedPublisher = newLevelReachedPublisher;
            SubscribeToMessages();
        }

        public void Initialize()
        {
        }

        private async void LoadScene(string sceneName, Action afterSceneLoadAction)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            afterSceneLoadAction.Invoke();
        }

        private Scene GetActiveScene() => SceneManager.GetActiveScene();

        private void SubscribeToMessages()
        {
            _scoreChangedSubscriber.Subscribe(OnScoreChanged).AddTo(BagBuilder);
        }

        private void OnScoreChanged(ScoreChangedMessage scoreChangedMessage) =>
            HandleLevelProgression(scoreChangedMessage.Score);

        private void HandleLevelProgression(int score)
        {
            const string finalSceneName = "Scene02";

            if (score < 5 || GetActiveScene().name == finalSceneName) return;

            LoadScene(finalSceneName,
                () => _newLevelReachedPublisher.Publish(new NewLevelReachedMessage { CurrentScore = score }));
        }
    }
}