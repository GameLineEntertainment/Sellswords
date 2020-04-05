using UnityEngine;

namespace OldSellswords
{
	public class LevelSetup : MonoBehaviour
	{
		[SerializeField] private LevelSettings _levelSettings;
		[SerializeField] private Transform _redSpawnPosition;
		[SerializeField] private Transform _greenStartPosition;
		[SerializeField] private Transform _blueStartPosition;
		private InputPCSystem _inputPCSystem;
		private RuningEnergySystem _runingEnergySystem;
		private CharacterMoveSystem _characterMoveSystem;
		private CharacterAttackSystem _characterAttackSystem;
		private SpawnerEnemiesSystem _spawnerEnemiesSystem;
		private MissionSystem _missionSystem;

		private void Start()
		{
			InstantiateCharacter();

			FindObjectOfType<GameLevel>().OnCharsWasLoaded();

			ProcessingUpdate.Default = new ProcessingUpdate();

			_runingEnergySystem = new RuningEnergySystem();
			_runingEnergySystem.OnAwake();
			ProcessingUpdate.Default.Add(_runingEnergySystem);

			_characterAttackSystem = new CharacterAttackSystem();
			_characterAttackSystem.OnAwake();
			ProcessingUpdate.Default.Add(_characterAttackSystem);

			_characterMoveSystem = new CharacterMoveSystem();
			_characterMoveSystem.OnAwake();
			_characterMoveSystem.OnCharacterChange += _runingEnergySystem.DecreaseEnergy;
			ProcessingUpdate.Default.Add(_characterMoveSystem);


			_inputPCSystem = new InputPCSystem();
			_inputPCSystem.OnLeftChange += _characterMoveSystem.Left;
			_inputPCSystem.OnRightChange += _characterMoveSystem.Right;
			ProcessingUpdate.Default.Add(_inputPCSystem);

			_spawnerEnemiesSystem = new SpawnerEnemiesSystem();
			_spawnerEnemiesSystem.OnAwake();

			_missionSystem = new MissionSystem();
			_missionSystem.OnAwake();
		}

		private void InstantiateCharacter()
		{
			var red = Instantiate(_levelSettings.RedCharacter.PrefabPlayable, 
				_redSpawnPosition.position, _redSpawnPosition.rotation);
			red.GetComponent<CharacterComponent>().Index = 0;

			var green = Instantiate(_levelSettings.GreenCharacter.PrefabPlayable, _greenStartPosition.position, _greenStartPosition.rotation);
			green.GetComponent<CharacterComponent>().Index = 1;
			
			var blue = Instantiate(_levelSettings.BlueCharacter.PrefabPlayable, _blueStartPosition.position, _blueStartPosition.rotation);
			blue.GetComponent<CharacterComponent>().Index = 2;
		}

		private void OnDestroy()
		{
			_characterMoveSystem.OnCharacterChange -= _runingEnergySystem.DecreaseEnergy;
			_inputPCSystem.OnLeftChange -= _characterMoveSystem.Left;
			_inputPCSystem.OnRightChange -= _characterMoveSystem.Right;

			_spawnerEnemiesSystem.Dispose();
			_missionSystem.Dispose();
		}
	}
}
