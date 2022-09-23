using UnityEngine;

namespace Factories
{
    public sealed class MonoBehaviourFactory : MonoBehaviour
    {
        public static TMonoBehaviour Create<TMonoBehaviour>(ObjectScopeType scopeType)
            where TMonoBehaviour : MonoBehaviour
        {
            var container = new GameObject(typeof(TMonoBehaviour).Name);

            var instance = container.AddComponent<TMonoBehaviour>();

            if (scopeType == ObjectScopeType.Game)
                DontDestroyOnLoad(container);

            return instance;
        }
    }
}