using System.Collections.Generic;
using UnityEngine;

namespace CombatScene
{
    public sealed class MainSystem : MonoBehaviour
    {
        public static MainSystem Main;

        private List<IUpdate> _update = new List<IUpdate>();
        private List<IUpdateLate> _updateLate = new List<IUpdateLate>();
        private List<IUpdateFixed> _updateFixed = new List<IUpdateFixed>();

        InputSystem _input = new InputSystem();
        CreateHeroes _spawnHeroes = new CreateHeroes();
        EnemySpawner _spawnEnemy = new EnemySpawner();
        SpecialFunc _specilaFunc = new SpecialFunc();
        PowerUI _powerUI = new PowerUI();


        private void Awake()
        {
            Main = this;
            
            Run(_spawnHeroes);
            Run(_specilaFunc);
            Run(_powerUI);
            Run(_input);
        }

        private void Start()
        {
            NewUpdate(_input);
            NewUpdate(_specilaFunc);
            NewUpdate(_powerUI);

            Run(_spawnEnemy);
            foreach (EnemyModel en in FindObjectsOfType<EnemyModel>())
            {
                NewUpdate(en);
            }
            NewUpdate(_spawnEnemy);

            foreach (CharacterModel pl in FindObjectsOfType<CharacterModel>())
            {
                NewUpdate(pl);
            }
            NewUpdate(new Meteor());
        }

        private void Update()
        {
            foreach (IUpdate upd in _update)
            {
                if (upd == null) _update.Remove(upd);
                upd.OnUpdate();
            }
        }

        private void FixedUpdate()
        {
            if (_updateFixed.Count < 1) return;
            foreach (IUpdateFixed upd in _updateFixed)
            {
                upd.OnUpdateFixed();
            }
        }

        private void LateUpdate()
        {
            if (_updateLate.Count < 1) return;
            foreach (IUpdateLate upd in _updateLate)
            {
                upd.OnUpdateLate();
            }
        }
        public void Run(IAwake awake)
        {
            awake.OnAwake();
        }

        public void NewUpdate(object update)
        {
            switch(update)
            {
                case IUpdate _:
                    {
                        _update.Add(update as IUpdate);
                        break;
                    }
                case IUpdateFixed _:
                    {
                        _updateFixed.Add(update as IUpdateFixed);
                        break;
                    }
                case IUpdateLate _:
                    {
                        _updateLate.Add(update as IUpdateLate);
                        break;
                    }
            }
        }

        public void RemoveUpdate(object update)
        {
            switch (update)
            {
                case IUpdate _:
                    {
                        _update.Remove(update as IUpdate);
                        break;
                    }
                case IUpdateFixed _:
                    {
                        _updateFixed.Remove(update as IUpdateFixed);
                        break;
                    }
                case IUpdateLate _:
                    {
                        _updateLate.Remove(update as IUpdateLate);
                        break;
                    }
            }
        }

    }
}