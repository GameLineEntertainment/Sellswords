using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace Sellswords
{
    public class MainMenuScenes : BaseMenu///скрипт должен висеть на любом объекте в сцене
    {
        [SerializeField] private ButtonUi _locationKollokvium;///ссылка на кнопку
        [SerializeField] private ButtonUi _locationForest;
        [SerializeField] private ButtonUi _shop;
        [SerializeField] private ButtonUi _camp;
        [SerializeField] private ButtonUi _inventory;
        [SerializeField] private ButtonUi _play;
        [SerializeField] private ButtonUi _towerMenu;
        [SerializeField] private ButtonUi _heroeBlue;
        [SerializeField] private ButtonUi _heroeBlue1;
        [SerializeField] private ButtonUi _сurrentHealthBlue;
        [SerializeField] private ButtonUi _heroeGreen;
        [SerializeField] private ButtonUi _heroeGreen1;
        [SerializeField] private ButtonUi _сurrentHealthGreen;
        [SerializeField] private ButtonUi _heroeRed;
        [SerializeField] private ButtonUi _heroeRed1;
        [SerializeField] private ButtonUi _сurrentHealthRed;
        [SerializeField] private ButtonUi _ImageLoot1;
        [SerializeField] private ButtonUi _ImageLoot2;
        [SerializeField] private GameObject _buttonsTower;
        [SerializeField] private GameObject _selectMission;
        [SerializeField] private Sprite _setKollokviumImage;
        [SerializeField] private Sprite _setForestImage;
        private int _locationInd;// индекс загружаемой сцены

        private void Start()
        {          
            #region
            _locationKollokvium.GetControl.onClick.AddListener(delegate /// логика при клике
            {
                _locationInd = 4;//выбор текущий сцены
            _selectMission.GetComponent<Image>().sprite = _setKollokviumImage;// загрузка партрета уровня
            Show(_selectMission);// показывает окно с выбраной миссией
        });
            _locationForest.GetControl.onClick.AddListener(delegate
            {
                _locationInd = 5;
                _selectMission.GetComponent<Image>().sprite = _setForestImage;
                Show(_selectMission);
            });
            _shop.GetControl.onClick.AddListener(delegate
            {
                //SceneManager.LoadScene(2);// вызов сцены(Меню Shop)
        });
            _inventory.GetControl.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(2);
            });
            _camp.GetControl.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(1);
            });
            _play.GetControl.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(_locationInd);
            });
            _towerMenu.GetControl.onClick.AddListener(delegate
            {
                if (_selectMission.activeSelf)
                {
                    Hide(_selectMission);
                }
                Show(_buttonsTower);/// показывает кнопочки ивенторя,магазина,лагеря
        });
            #endregion
            // реализовать смена героев
            #region
            //_heroeBlue.GetControl.onClick.AddListener(delegate
            //{

            //});
            //_heroeBlue1.GetControl.onClick.AddListener(delegate
            //{

            //});
            //_сurrentHealthBlue.GetControl.onClick.AddListener(delegate
            //{

            //});
            //_heroeGreen.GetControl.onClick.AddListener(delegate
            //{

            //});
            //_heroeGreen1.GetControl.onClick.AddListener(delegate
            //{

            //});
            //_сurrentHealthGreen.GetControl.onClick.AddListener(delegate
            //{

            //});
            //_heroeRed.GetControl.onClick.AddListener(delegate
            //{

            //});
            //_heroeRed1.GetControl.onClick.AddListener(delegate
            //{

            //});
            //_сurrentHealthRed.GetControl.onClick.AddListener(delegate
            //{

            //});
            #endregion
        }
        public override void Hide(GameObject _gameObject) /// метод скрытия  
        {
            if (!IsShow) return;
            _gameObject.gameObject.SetActive(false);
            IsShow = false;
        }
        public override void Show(GameObject _gameObject) // метод показа
        {
            if (IsShow)
            {
                Hide(_gameObject);
            }
            else
            {
                _gameObject.gameObject.SetActive(true);
                IsShow = true;
            }
        }
    }
}