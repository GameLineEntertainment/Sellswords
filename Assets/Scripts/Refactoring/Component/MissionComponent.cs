using System;
using System.Collections.Generic;
using System.Linq;
using Sellswords;
using UnityEngine;

namespace OldSellswords
{
	public class MissionComponent : MonoBehaviour
	{
		//todo вознаграждение
		public bool IsComplited;
        public Arena Arena;
		public MissionGenerator MissionGenerator;
        public List<MissionInfo> MissionInfos;


        private void Awake()
        {
            MissionInfos = new List<MissionInfo>(5);
            MissionGenerator.Generator(Arena);
            if (MissionGenerator.DataArens.ContainsKey(Arena))
            {
                foreach (var enemy in MissionGenerator.DataArens[Arena])
                {
                    var countEnemies = MissionGenerator.DataArens[Arena].Count(e => e.name == enemy.name && 
                                       e.ColorGroup == enemy.ColorGroup);

                    var mission = new MissionInfo
                    {
                        Name = enemy.Name,
                        Color = enemy.ColorGroup,
                        Count = countEnemies
                    };
                    MissionInfos.Add(mission);
                }
                MissionInfos = MissionInfos.Distinct(new DistinctMissionInfoComparer()).ToList();
                MissionInfos = MissionInfos.OrderBy(i => i.Name).ToList();
            }
        }
	}

    [Serializable]
	public struct MissionInfo
	{
		public string Name;
		public ColorGroup Color;
		public int Count;
		public int Killed;

        public override string ToString()
        {
            return $"Name = {Name} Color = {Color} Count = {Count}";
        }
    }

    public class DistinctMissionInfoComparer : IEqualityComparer<MissionInfo>
    {

        public bool Equals(MissionInfo x, MissionInfo y)
        {
            return x.Name == y.Name &&
                   x.Color == y.Color;
        }

        public int GetHashCode(MissionInfo obj)
        {
            return obj.Name.GetHashCode() ^
                   obj.Color.GetHashCode();
        }
    }
}