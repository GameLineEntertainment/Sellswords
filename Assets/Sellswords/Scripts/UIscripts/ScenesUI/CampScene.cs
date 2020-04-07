using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


namespace Sellswords
{
    public class CampScene : BaseMenu///скрипт должен быть на конвосе
    {
        public TextMeshProUGUI PlaceNameCharacter;/// поле для вывода Name активного героя
        public TextMeshProUGUI PlaceLvLCharacter;/// поле для вывода LvL активного героя
        public TextMeshProUGUI MaxHp;
        public TextMeshProUGUI CurrentHp;
        public TextMeshProUGUI Ability;
        public TextMeshProUGUI Damage;
        [SerializeField] private GameObject _buttonsTower;// кнопки меню город,лагерь,магазин
        [SerializeField] private ButtonUi _shop; //кнопочка магазина
        [SerializeField] private ButtonUi _inventory;
        [SerializeField] private ButtonUi _city;
        [SerializeField] private ButtonUi _towerMenu;
        [SerializeField] private ButtonUi _miniGame;
        [SerializeField] private ButtonUi _levelButton;
        [SerializeField] private GameObject _levelPanel;
        [SerializeField] private ButtonUi _statsButton;
        [SerializeField] private GameObject _statsPanel;
        [SerializeField] private ButtonUi _upGradeButton;
        [SerializeField] private GameObject _charahtersBox;
        [SerializeField] private GameObject _characterAvatarBox;// префаб модели аватара resources/ui "NewcharachterAvatar"
        private bool _isUpGrade;//можно улучшить или нет
        private Color _colorDefault;
        private Color _colorActive;
        TestInventory Inventory; // фективный класс хранивший инвентарь и его количество ЗАМЕНИТЬ
        TestGrouPHeroes testGrouPHeroes;// фективный класс хранивший количество героев имеющегося у игрока  ЗАМЕНИТЬ
        private void Start()
        {             
            testGrouPHeroes = TestGrouPHeroes.testGrouPHeroes;//список героев фективный ЗАМЕНИТЬ
            RectTransform rectTransform = _charahtersBox.GetComponent<RectTransform>(); ///скролл бокс
            rectTransform.sizeDelta = new Vector2(40, ((testGrouPHeroes.testHeroes.Count) * 19));// размер под количество героев
            for (int i = 0; i < testGrouPHeroes.testHeroes.Count; i++)
            {
                GameObject _characters = Instantiate(_characterAvatarBox, new Vector3(_charahtersBox.transform.position.x, (_charahtersBox.transform.position.y + (rectTransform.sizeDelta.y * 2) + (testGrouPHeroes.testHeroes.Count * 5)) - (i * 90), 0), Quaternion.identity) as GameObject;
                _characters.transform.SetParent(_charahtersBox.transform);
                CharacterAvatar _charh = _characters.GetComponentInChildren<CharacterAvatar>();
                _charh._hpBar._hp = testGrouPHeroes.testHeroes[i].HP;
                _charh.AddAvatar(testGrouPHeroes.testHeroes[i]);
            }
            Inventory = TestInventory.instance;
            SlotUI[] _slots = GetComponentsInChildren<SlotUI>(); //все слоты
            for (int i = 0; i < _slots.Length; i++)
            {
                if (i < Inventory.testItems.Count)/// распределяем объекты в слоты
                {
                    _slots[i].AddItem(Inventory.testItems[i]);
                }
            }
            _colorDefault = _levelButton.GetImage.color;
            _colorActive = new Color32(83, 92, 55, 255);
            Show(_statsPanel);
            _statsButton.GetImage.color = _colorActive;
            Hide(_levelPanel);
            _isUpGrade = true;//добавить проверку на апгрейд
            if (_isUpGrade)// если можно
            {
                _upGradeButton.GetImage.color = _colorActive;
            }
            _shop.GetControl.onClick.AddListener(delegate
            {
                //SceneManager.LoadScene(2);// вызов сцены(Меню Shop)
            });
            _city.GetControl.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(0);
            });
            _inventory.GetControl.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(2);
            });
            _towerMenu.GetControl.onClick.AddListener(delegate
            {
                Show(_buttonsTower);/// показывает кнопочки ивенторя,магазина,лагеря
            });
            _levelButton.GetControl.onClick.AddListener(delegate
            {
                _statsButton.GetImage.color = _colorDefault;
                _levelButton.GetImage.color = _colorActive;
                Show(_levelPanel);
                Hide(_statsPanel);
            });
            _statsButton.GetControl.onClick.AddListener(delegate
            {
                _statsButton.GetImage.color = _colorActive;
                _levelButton.GetImage.color = _colorDefault;
                MaxHp.text = "три";//activ.character.maxhp
                CurrentHp.text = "два";//вывод на панель стат
                Ability.text = "ебливити";//вывод на панель стат
                Damage.text = "дэмэдж";//вывод на панель стат
                Show(_statsPanel);
                Hide(_levelPanel);
            });
            _upGradeButton.GetControl.onClick.AddListener(delegate
            {
                _isUpGrade = false;/// add проверка на возможность апгрейд что бы не делать постоянно в Update о- оптимизация           
                for (int i = 0; i < _slots.Length; i++) // пересчет инвенторя что бы не делать постоянно в Update о- оптимизация
                {
                    if (i < Inventory.testItems.Count)/// распределяем объекты в слоты
                    {
                        _slots[i].AddItem(Inventory.testItems[i]);
                    }
                }
                if (!_isUpGrade)/// цвет кнопки если апгрейд не возможен
                {
                    _upGradeButton.GetImage.color = Color.black;
                }
            });
            _miniGame.GetControl.onClick.AddListener(delegate
            {
                //загрузка мини
            });
        }
        public override void Hide(GameObject gameObject)
        {
            if (!IsShow) return;
            gameObject.gameObject.SetActive(false);
            IsShow = false;
        }
        public override void Show(GameObject gameObject)
        {
            if (IsShow)
            {
                Hide(gameObject);
            }
            else
            {
                gameObject.gameObject.SetActive(true);
                IsShow = true;
            }
        }
    }
}
