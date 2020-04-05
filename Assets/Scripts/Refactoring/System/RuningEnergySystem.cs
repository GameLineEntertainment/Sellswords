using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OldSellswords
{
	public class RuningEnergySystem : IAwake, ITick
	{
		private EnergyUI _energyUi;

		private Mini_Artificial_Director _artDir; // Ссылка на режиссёра

		private EnergyComponent _energyComponent;
		private CharacterComponent[] _characters;
		private TaskManager _taskManager;

		public void OnAwake()
		{
			_taskManager = TaskManager.CreateTaskManager();
			_artDir = Object.FindObjectOfType<Mini_Artificial_Director>();
			_characters = Object.FindObjectsOfType<CharacterComponent>();

			_energyComponent = Object.FindObjectOfType<EnergyComponent>();
			_energyComponent.Speed = new []
			{
				new CharSpeed{CastSpeed = 1, MoveSpeed = 4},
				new CharSpeed{CastSpeed = 1, MoveSpeed = 4},
				new CharSpeed{CastSpeed = 1, MoveSpeed = 4},
				new CharSpeed{CastSpeed = 1, MoveSpeed = 4},
			};
			_energyUi = Object.FindObjectOfType<EnergyUI>();
		}

		public void DecreaseEnergy()
		{
			if (_energyComponent.IsRest)
			{
				_energyComponent.IsRest = false;
				_energyComponent.CurrentEnergy -= _energyComponent.TurnCost;         // отнимаем силы    
				if (_energyComponent.CurrentEnergy < 0)
					_energyComponent.CurrentEnergy = 0;
				return;
			}
			_taskManager.AddTask(Wait(_energyComponent.CooldownTime));
			_taskManager.AddTask(Cooldown());

			//CancelInvoke("Cooldown");
			//Invoke("Cooldown", CooldownTime);
		}

		public void Tick()
		{
			_energyUi.Fill = _energyComponent.CurrentEnergy / 10;

			if (_energyComponent.CurrentEnergy > 7 && _energyComponent.StateEnergy != StateEnergy.Normal)
			{
				_energyUi.SetColor(Color.green);

				_artDir.AntiCheat = false;
				_energyComponent.StateEnergy = StateEnergy.Normal;
				ChangeSpeed();
			}

			else if (_energyComponent.CurrentEnergy <= 7 && _energyComponent.CurrentEnergy > 3 && _energyComponent.StateEnergy != StateEnergy.Easy)
			{
				_energyUi.SetColor(Color.yellow);

				_artDir.AntiCheat = false;
				_energyComponent.StateEnergy = StateEnergy.Easy;
				ChangeSpeed();
			}

			else if (_energyComponent.CurrentEnergy <= 3 && _energyComponent.StateEnergy != StateEnergy.Medium)
			{
				_energyUi.SetColor(Color.red);

				_energyComponent.StateEnergy = StateEnergy.Medium;

				ChangeSpeed();
			}

			else if (_energyComponent.CurrentEnergy == 0 && _energyComponent.StateEnergy != StateEnergy.Hard)
			{

				_energyComponent.StateEnergy = StateEnergy.Hard;

				_artDir.AntiCheat = true; // todo Переписать 
				ChangeSpeed();
			}

			if (_energyComponent.IsRest && _energyComponent.CurrentEnergy < EnergyComponent.MaxEnergy)
			{
				_energyComponent.CurrentEnergy += _energyComponent.RestoreSpeed * Time.deltaTime;

				if (_energyComponent.CurrentEnergy > EnergyComponent.MaxEnergy)
					_energyComponent.CurrentEnergy = EnergyComponent.MaxEnergy;
			}

			// если ещё на начали отдыхать начинаем систему отдыха
			if (!_energyComponent.CDprogress && !_energyComponent.IsRest)
			{
				_energyComponent.CDprogress = true; // запущена система отдыха

				_taskManager.AddTask(Wait(_energyComponent.CooldownTime));
				_taskManager.AddTask(Cooldown());
			}
		}

		private void ChangeSpeed()
		{
			for (int i = 0; i < _characters.Length; i++)
			{
				_characters[i].moveSpeed = (int)_energyComponent.Speed[(int)_energyComponent.StateEnergy].MoveSpeed;
				_characters[i].CoulDownTime = _energyComponent.Speed[(int)_energyComponent.StateEnergy].CastSpeed;
			}
		}

		private IEnumerator Wait(float time)
		{
			yield return new WaitForSeconds(time);
		}

		private IEnumerator Cooldown()
		{
			_energyComponent.IsRest = true;
			_energyComponent.CDprogress = false;
			yield return null;
		}
	}
}