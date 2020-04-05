using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OldSellswords
{
	public class MissionSystem : IAwake, IDisposable
	{
		private MissionComponent _mission;
		private SpawnerEnemiesComponent _spawnerEnemiesComponent;

		public void OnAwake()
		{
			_mission = Object.FindObjectOfType<MissionComponent>();
			_spawnerEnemiesComponent = Object.FindObjectOfType<SpawnerEnemiesComponent>();

			_spawnerEnemiesComponent.EnemiesOnStage.CollectionChanged += EnemiesOnStageOnCollectionChanged;
		}

		private void EnemiesOnStageOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (!(sender is ObservableCollection<MiniEnemyAI> item)) return;
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					item[0].OnKilledByPlayerChange += OnDieChange;
					break;
			}
		}

		private void OnDieChange(MiniEnemyAI obj)
		{
			for (var i = 0; i < _mission.MissionInfos.Count; i++)
			{
				var info = _mission.MissionInfos[i];
				if (info.Name == obj.Name)
				{
					if (info.Color == obj.EnemyColor)
					{
						var killed = info.Killed;
						killed++;
						info.Killed = killed;
						if (info.Killed >= info.Count)
						{
							Debug.Log(info.Killed);
							_mission.MissionInfos.Remove(info);
						}
						else
						{
							_mission.MissionInfos[i] = info;
						}
					}
				}
			}

			Debug.Log(_mission.MissionInfos.Count);
			if (_mission.MissionInfos.Count == 0)
			{
				_mission.IsComplited = true;
			}

			obj.OnDieChange -= OnDieChange;
		}

		public void Dispose()
		{
			_spawnerEnemiesComponent.EnemiesOnStage.CollectionChanged -= EnemiesOnStageOnCollectionChanged;
		}
	}
}