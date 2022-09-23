using Factories;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneHandler : MonoBehaviour
{
    private static SceneHandler _instance;

    private SceneHandler()
    {
    }

    public static SceneHandler Instance =>
        _instance ??= MonoBehaviourFactory.Create<SceneHandler>(ObjectScopeType.Game);

    public void LoadScene(string sceneName, Action afterLoadAction) =>
        StartCoroutine(LoadSceneRoutine(sceneName, afterLoadAction));

    public Scene GetActiveScene() => SceneManager.GetActiveScene();

    private void Awake()
    {
        if (_instance == null || _instance == this) return;
        Destroy(this);
    }

    private IEnumerator LoadSceneRoutine(string sceneName, Action afterLoadAction)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!asyncOperation.isDone) yield return null;

        afterLoadAction.Invoke();
    }
}