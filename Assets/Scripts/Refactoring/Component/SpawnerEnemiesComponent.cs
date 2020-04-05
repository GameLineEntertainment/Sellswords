using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace OldSellswords
{
	public class SpawnerEnemiesComponent : MonoBehaviour
	{
		public Vector2 SpeedEnemie;
		public MiniEnemyAI[] EnemyObjects;
		public Transform StartPos;
		public int StartDelay;
		public int RefreshCheckTime;
		[HideInInspector] public int NumberOfLiveMonsters;
		[HideInInspector] public ObservableCollection<MiniEnemyAI> EnemiesOnStage = new ObservableCollection<MiniEnemyAI>();
		[HideInInspector] public CharacterComponent[] CharacterComponents;
	}
}