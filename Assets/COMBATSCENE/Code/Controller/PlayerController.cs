using UnityEngine;

namespace CombatScene
{
    public class PlayerController : IAwake
    {
        public static CharacterModel[] Player = new CharacterModel[3];

        private Transform HeroPlace;

        private Vector3 _mainPos;
        private Vector3 _leftPos;
        private Vector3 _rightPos;


        public void OnAwake()
        {
            HeroPlace = Object.FindObjectOfType<CharacterModel>().transform.parent;

            _mainPos = HeroPlace.GetChild(1).position;
            _leftPos = HeroPlace.GetChild(0).position;
            _rightPos = HeroPlace.GetChild(2).position;

        }


        public void ChangePosition(bool isClockwise)
        {
            foreach (CharacterModel hero in Player)
            {
                if (hero.IsMove) return;
            }

            if (isClockwise)
            {
                SwitchHeroes(_leftPos, _rightPos);
            }
            else
            {
                SwitchHeroes(_rightPos, _leftPos);
            }
        }

        public void SwitchHeroes(Vector3 newMain, Vector3 lastMain)
        {
            for (int i = 0; i < Player.Length; i++)
            {
                if (Player[i].transform.position == newMain)
                {
                    Player[i].Move(_mainPos, true);
                }
                if (Player[i].transform.position == _mainPos)
                {
                    Player[i].Move(lastMain, false);
                }
                if (Player[i].transform.position == lastMain)
                {
                    Player[i].Move(newMain, false);
                }
            }
        }
    }
}
