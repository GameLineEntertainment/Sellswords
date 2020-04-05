using UnityEngine;

namespace CombatScene
{
    public class CreateHeroes : IAwake
    {
        private PlayerController _playerController;
        private CharManager.Char[] _character;
        private GameObject _spawnPlace;

        public void OnAwake()
        {
            var Player = new GameObject();
            Player.name = "Player";

            _character = Object.FindObjectOfType<Manager>().CharactersSettings.Characters;
            _spawnPlace = GameObject.FindGameObjectWithTag("HeroSpawn");

            for (int i = 0; i < 3; i++)
            {
                var NewHero = Object.Instantiate(_character[i].Prefab, _spawnPlace.transform.GetChild(i).position, Quaternion.identity, Player.transform);
                NewHero.AddComponent<CharacterModel>();
                NewHero.name = _character[i].Prefab.name;
            }
        }
    }
}
