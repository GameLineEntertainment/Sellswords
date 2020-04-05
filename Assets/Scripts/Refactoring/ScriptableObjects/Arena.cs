using UnityEngine;

namespace OldSellswords
{
    [CreateAssetMenu(fileName = "arena", menuName = "Scriptable Object/Data/Arena")]
    public sealed class Arena : BaseData
    {
        public Enemy[] Enemies;
    }
}