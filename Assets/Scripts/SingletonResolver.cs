using UnityEngine;

public sealed class SingletonResolver : MonoBehaviour
{
    public static T Resolve<T>(T instance) where T : MonoBehaviour
    {
        if (instance != null) return instance;

        instance = FindObjectOfType<T>();

        if (instance != null) return instance;

        var container = new GameObject(typeof(T).Name);
        
        instance = container.AddComponent<T>();

        DontDestroyOnLoad(instance);

        return instance;
    }
}