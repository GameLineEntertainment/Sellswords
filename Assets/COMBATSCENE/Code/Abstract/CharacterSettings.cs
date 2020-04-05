using System.Collections.Generic;
using UnityEngine;

namespace CombatScene
{
    public abstract class CharacterSettings : MonoBehaviour
    {
        public Dictionary<int, GameObject[]> Projectiles { get; private set; } = new Dictionary<int, GameObject[]>();
        public SpellManager.SpellSettings[] ProjectilesSettings { get; private set; } = new SpellManager.SpellSettings[3];

        protected List<Transform> _enemies = new List<Transform>();

        private CharManager.CharSettings _charSettings;
        private CharManager.Char[] _char;
        private CharManager _chars;

        protected SpellManager.SpellSettings[] _spells;
        private SpellManager _spellManager;

        protected Animator _anim;
        protected Rigidbody _rig;

        protected int _id;
        protected float _hp;
        protected GameObject _spell;
        protected int _spellId;
        protected Vector3 _spellPlace;
        protected float _attackDelay;

        protected bool _isDead;

        public float Speed { get; set; }
        public float BasicSpeed { get; set; }

        public float Damage { get; set; }
        public float BasicDamage { get; set; }

        public virtual void Awake()
        {
            _anim = GetComponentInChildren<Animator>();
            _rig = GetComponentInChildren<Rigidbody>();

            _chars = FindObjectOfType<Manager>().CharactersSettings;
            _charSettings = _chars.Settings;
            _char = _chars.Characters;

            _spellManager = FindObjectOfType<Manager>().SpellsSettings;
            _spells = FindObjectOfType<Manager>().SpellsSettings.Spell;
            Settings();
        }
        public virtual void Start()
        {
            Settings();
        }

        public void MainSettings(float hp, float speed, float damage, float attackDelay)
        {
            _hp = hp;
            Damage = damage;
            BasicDamage = Damage;
            Speed = speed;
            BasicSpeed = Speed;
            _attackDelay = attackDelay;
        }

        public void SpellOffsetPos(Vector3 newPos)
        {
            _spellPlace = newPos;
        }

        public void SpellMainPos(int charId, int placeId)
        {
            if (charId == _id)
            {
                _spellPlace = _spellManager.GetSpellPosition(placeId);
                InBuildSceneEditor.Main.GetSpellPlaceOffset(_id, _spellPlace);
            }
        }

        protected virtual void Settings()
        {
            _hp = _charSettings.Health;
            Speed = _charSettings.Speed;
            BasicSpeed = Speed;
            Damage = _charSettings.Damage;
            BasicDamage = Damage;
            _attackDelay = _charSettings.AttackDelay;

            for (int i = 0; i < _char.Length; i++)
            {
                if (gameObject.name == _char[i].Prefab.name)
                {
                    _id = (int)_char[i].Type;
                    for (int j = 0; j < _spells.Length; j++)
                    {
                        if (_spells[j].Id == _char[i].UseSpellId)
                        {
                            _spellId = _spells[j].Id;
                            ProjectilesSettings[_id] = _spells[j];

                            CreatePoolProjectiles(_spells[j].Prefab,
                                                  _spells[j].PoolSize,
                                                  _spellManager.GetSpellPosition((int)_spells[j].MainPosition));

                            _spellPlace = _spellManager.GetSpellPosition((int)_spells[j].MainPosition);
                            InBuildSceneEditor.Main.GetSpellPlace(_id, (int)_spells[j].MainPosition);
                            InBuildSceneEditor.Main.GetSpellPlaceOffset(_id, _spellPlace);
                        }
                    }
                }
            }
        }

        public void CreatePoolProjectiles(GameObject poolObject, int poolSize, Vector3 pos)
        {
            var pool = new Pool();
            GameObject[] tempArray;
            tempArray = pool.StartPool(poolObject, poolSize, pos, Quaternion.identity, 0);
            if (!Projectiles.ContainsKey(_id))
            {
                Projectiles.Add(_id, tempArray);
                foreach (GameObject proj in Projectiles[_id])
                {
                    proj.name = _id.ToString();
                    proj.SetActive(false);
                }
            }


        }

        protected virtual bool EnemyList()
        {
            _enemies.Clear();
            foreach (EnemyModel enemy in FindObjectsOfType<EnemyModel>())
            {
                if (!enemy.IsDead)
                {
                    _enemies.Add(enemy.transform);
                }
            }
            if (_enemies.Count < 1) return false;
            return true;
        }
    }
}
