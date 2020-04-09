﻿using UnityEngine;
using UnityEngine.SceneManagement;


namespace Sellswords
{
    public class CampScene : BaseMenu///скрипт должен быть на конвосе
    {
        [SerializeField] private StatPanel _statPanel;
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
        private Color _colorDefault;
        private Color _colorActive;
        private bool _isUpGrade;//можно улучшить или нет
        public  Transform CharacterRotation;
        public static CampScene Intance;
        TestInventory Inventory; // фективный класс хранивший инвентарь и его количество ЗАМЕНИТЬ
        public TestGrouPHeroes testGrouPHeroes;// фективный класс хранивший количество героев имеющегося у игрока  ЗАМЕНИТЬ
        void Awake()
        {
            Intance = this;
        }
        private void Start()
        {            
            testGrouPHeroes = TestGrouPHeroes.testGrouPHeroes;//список героев фективный ЗАМЕНИТЬ
            testGrouPHeroes.SelectedCharacter = testGrouPHeroes.testHeroes[0];
            Instantiate(testGrouPHeroes.SelectedCharacter._prefab, CampScene.Intance.CharacterRotation).SetActive(true);
            RectTransform _rectTransform = _charahtersBox.GetComponent<RectTransform>(); ///скролл бокс
            _rectTransform.sizeDelta = new Vector2(40, ((testGrouPHeroes.testHeroes.Count) * 19));// размер под количество героев
            for (int i = 0; i < testGrouPHeroes.testHeroes.Count; i++)
            {               
                GameObject _characters = Instantiate(_characterAvatarBox, new Vector3(_charahtersBox.transform.position.x, (_charahtersBox.transform.position.y + (_rectTransform.sizeDelta.y * 2) + (testGrouPHeroes.testHeroes.Count * 5)) - (i * 90), 0), Quaternion.identity) as GameObject;
                _characters.transform.SetParent(_charahtersBox.transform);
                CharacterAvatar _avatar = _characters.GetComponentInChildren<CharacterAvatar>();
                _avatar.HpBar._hp = testGrouPHeroes.testHeroes[i].HP;
                _avatar.AddAvatar(testGrouPHeroes.testHeroes[i]);
            }           
            Inventory = TestInventory.instance;
            SlotUI[] _slots = GetComponentsInChildren<SlotUI>(); //все слоты
            for (int i = 0; i < _slots.Length; i++)
            {
                Debug.Log(_slots.Length);
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
        public void Update()
        {
            _statPanel.ShowStats(testGrouPHeroes);
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
