using System.Linq;
using UnityEngine;

namespace OldSellswords
{
	public class CharacterAttackSystem: ITick, IAwake
	{
		private CharacterComponent[] _characters;
		private SpawnerEnemiesComponent _spawnerEnemiesComponent;
		private Transform _enemy;

		public void OnAwake()
		{
			_characters = Object.FindObjectsOfType<CharacterComponent>();
			_spawnerEnemiesComponent = Object.FindObjectOfType<SpawnerEnemiesComponent>();
		}

		public void Tick()
		{
			foreach (var character in _characters)
			{
				if (character.CanAttack)
				{
					ChekPosition(character);
				}
			}
		}

		private void ChekPosition(CharacterComponent character )         // проверяем позицию, где стоим, чтобы выбрать наших врагов.
		{
			if (!character.SortDistance) 
			{
				Attack(character);                 // запускаем ф-цию атаки
			}
			else
			{
				SortByDistance(character);
			}
			character.CanAttack = false;
		}

		private void SortByDistance(CharacterComponent character)
		{
			//var enemyes = GameObject.FindGameObjectsWithTag("Enemy").Select(o => o.transform).ToList();
			var enemyes = _spawnerEnemiesComponent.EnemiesOnStage.Select(o => o.transform).ToList();
			enemyes.Sort(delegate (Transform t1, Transform t2)
			{
				return Vector3.Distance(t1.position, character.myTransform.position).CompareTo(Vector3.Distance(t2.position, character.myTransform.position));
			});

			if(enemyes.Count < 1) return;
			_enemy = enemyes[0];                   //Элемент для стрелы (Ближайшая цель)
			
			Attack(character);             // запускаем ф-цию атаки
		}

		private void Attack(CharacterComponent character)     // Ф-ция атаки
		{
			if (character.CanAttack)  // проверка на кулдау
			{
				// yield return new WaitForSeconds(character.CoulDownTime); //считаем время
				if (character.Index != 2)
				{
					return;
				}

				character.MyAnim.Attack(true); //Запускаем анимацию атаки

				var num = Random.Range(0, character.Bullet.Length);
				var bull = Object.Instantiate(character.Bullet[num].Prjectile_Object,
					character.AttackPoint.position, Quaternion.identity); // todo перенести в пул

				bull.Settings = character.Bullet[num];
				bull.Target = _enemy;    
				bull.ParentChar = character.myTransform;

				bull.ProgectileStart();
			}
		}
	}
}