using System.IO;
using UnityEngine;


namespace Sellswords
{
    [CreateAssetMenu(fileName = "Data", menuName = "Sellswords/Data/Data")]
    public sealed class Data : ScriptableObject
    {
        #region PrivateData

        private const string BasePath = "Sellswords/Data";

        #endregion
        
        
        #region Fields

        [SerializeField] private string _characterDataPath;
        [SerializeField] private string _enemyDataPath;
        [SerializeField] private string _spellDataPath;
        private static Data _instance;
        private static CharacterData _characterData;
        private static EnemyData _enemyData;
        private static SpellData _spellData;

        #endregion
        
        
        #region Properties

        private static Data Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<Data>($"{BasePath}/{typeof(Data).Name}");
                }

                return _instance;
            }
        }
        
        public static CharacterData CharacterData
        {
            get
            {
                if (_characterData == null)
                {
                    _characterData = Load<CharacterData>($"{BasePath}/{Instance._characterDataPath}");
                }

                return _characterData;
            }
        }
        
        public static EnemyData EnemyData
        {
            get
            {
                if (_enemyData == null)
                {
                    _enemyData = Load<EnemyData>($"{BasePath}/{Instance._enemyDataPath}");
                }

                return _enemyData;
            }
        }
        
        public static SpellData SpellData
        {
            get
            {
                if (_spellData == null)
                {
                    _spellData = Load<SpellData>($"{BasePath}/{Instance._spellDataPath}");
                }

                return _spellData;
            }
        }

        #endregion


        #region Methods

        private static T Load<T>(string resourcesPath) where T : Object =>
            Resources.Load<T>(Path.ChangeExtension(resourcesPath, null));
    
        #endregion
    }
}