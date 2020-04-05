using System.Collections;
using UnityEngine;

namespace OldSellswords
{
	public class SpawnerEnemiesSystem :IAwake, System.IDisposable
	{
		private SpawnerEnemiesComponent _spawnerEnemiesComponent;
		private CharacterComponent[] _charactersComponent;
		private TaskManager _taskManager;

		public void OnAwake()
		{
			_spawnerEnemiesComponent = Object.FindObjectOfType<SpawnerEnemiesComponent>();
			_charactersComponent = Object.FindObjectsOfType<CharacterComponent>();
			_taskManager=TaskManager.CreateTaskManager();
			_taskManager.AddTask(Wait(_spawnerEnemiesComponent.StartDelay));
			_taskManager.AddTask(Spawn(_spawnerEnemiesComponent.RefreshCheckTime));
		}

		private IEnumerator Wait(float time)
		{
			yield return new WaitForSeconds(time);
		}

		private IEnumerator Spawn(float time)
		{
			while (true)
			{
				if (GameObject.FindGameObjectWithTag("Enemy") == null)//todo переписать 
				{
					CheckCharacter();
					Spawn();
				}
				yield return new WaitForSeconds(time);
			}
		}

		private void Spawn()
		{
			var en = Object.Instantiate(_spawnerEnemiesComponent.EnemyObjects[(int)CheckCharacter()],
				_spawnerEnemiesComponent.StartPos.position, _spawnerEnemiesComponent.StartPos.rotation);
			en.tag = "Enemy";
			en.MinSpeed = _spawnerEnemiesComponent.SpeedEnemie.x;
			en.MaxSpeed = _spawnerEnemiesComponent.SpeedEnemie.y;
			en.moveSpeed = Random.Range(en.MinSpeed, en.MaxSpeed); // Нахрена мобу ввобще знать свои пороги скорости? Бред!

			//todo сделать обратные опперации при смерти монстра 
			_spawnerEnemiesComponent.EnemiesOnStage.Add(en);
			_spawnerEnemiesComponent.NumberOfLiveMonsters++;

			en.OnDieChange += EnOnOnDieChange;
		}

		private void EnOnOnDieChange(MiniEnemyAI obj)
		{
			_spawnerEnemiesComponent.EnemiesOnStage.Remove(obj);
			_spawnerEnemiesComponent.NumberOfLiveMonsters--;
			obj.OnDieChange -= EnOnOnDieChange;
		}

		private ColorGroup CheckCharacter() // Записываем, кто где из девочек
		{
			foreach (var characterComponent in _charactersComponent)
			{
				if (characterComponent.Index == 2)
				{
				    var random = Random.Range(1, 100);
					switch (characterComponent.ColorGroup)
					{
						case ColorGroup.Red:
						{
							return random > 50 ? ColorGroup.Green : 
								ColorGroup.Blue;
						}
						case ColorGroup.Green:
						{
							return random > 50 ? ColorGroup.Red : 
								ColorGroup.Blue;
						}
						case ColorGroup.Blue:
						{
							return random > 50 ? ColorGroup.Red : 
								ColorGroup.Green;
						}
					}
				}
			}

			return ColorGroup.Red;
		}

		public void Dispose()
		{
			_taskManager.Clear();
		}
	}
}