using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sellswords
{
    public class EnemySpawnController : IInitializeController, IExecuteController, ICleanupController
    {
        #region PrivateData

        private readonly GameContext _context;
        private PoolObject<Enemy> _pool;
        private List<Enemy> _enemies = new List<Enemy>();
        private UsableServices _services;

        #endregion


        #region ClassLifeCycles

        public EnemySpawnController(GameContext context, UsableServices services)
        {
            _context = context;
            _services = services;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            _pool = new PoolObject<Enemy>(_context.EnemyData.Settings.PoolObjectPosition.transform.position,
                () => new Enemy(GetRandomEnemy(), _context.EnemyData.Settings.SpawnPosition, _context.CharacterData.Settings.ActivePosition, _services));
        }

        #endregion  


        #region IExecuteController

        public void Execute()
        {
            if (_context.NeedSpawnEnemy)
            {
                _enemies.Add(
                    _pool.GetObject(_context.EnemyData.Settings.SpawnPosition.transform.position));
            }

            _context.Enemies = _enemies?.OrderByDescending(enemy =>
                enemy.Transform.position.CalcDistance(_context.CharacterData.Settings.ActivePosition.position));
        }

        #endregion


        #region ICleanupController

        public void Cleanup()
        {
            _context.NeedSpawnEnemy = false;
        }

        #endregion


        #region Methods

        private EnemyObject GetRandomEnemy()
        {
            var enemiesObjects = _context.EnemyData.Enemies;
            var index = Random.Range(0, enemiesObjects.Length);
            return enemiesObjects[index];
        }

        #endregion
    }
}