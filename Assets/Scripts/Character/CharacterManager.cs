using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Character
{
    public sealed class CharacterManager : IInitializable
    {
        private readonly IObjectResolver _container;
        private readonly CharacterSettings _characterSettings;

        public CharacterManager(IObjectResolver container,
            CharacterSettings characterSettings)
        {
            _characterSettings = characterSettings;
            _container = container;
        }

        public void Initialize() =>
            Object.DontDestroyOnLoad(_container.Instantiate(_characterSettings.CharacterPrefab));
    }
}