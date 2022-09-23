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

    public static SceneHandler Instance
    {
        get
        {
            _instance = SingletonResolver.Resolve(_instance);
            return _instance;
        }
    }

    public void LoadScene(string sceneName, Action afterLoadAction) =>
        StartCoroutine(LoadSceneRoutine(sceneName, afterLoadAction));

    public Scene GetActiveScene() => SceneManager.GetActiveScene();

    private IEnumerator LoadSceneRoutine(string sceneName, Action afterLoadAction)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!asyncOperation.isDone) yield return null;

        afterLoadAction.Invoke();
    }
}