using System;
using UnityEngine;

namespace Coin
{
    [Serializable]
    public sealed class CoinSettings
    {
        [field: SerializeField] public GameObject CoinPrefab { get; private set; }
        [field: SerializeField] public int CoinCountLimit { get; set; } = 10;
    }
}