using Character;
using Coin;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "FirstApproach/Settings")]
    public sealed class GameSettings : ScriptableObject
    {
        [field: SerializeField] public CoinSettings CoinSettings { get; private set; }
        [field: SerializeField] public CharacterSettings CharacterSettings { get; private set; }
    }
}