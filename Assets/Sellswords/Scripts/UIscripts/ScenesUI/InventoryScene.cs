using UnityEngine;
using UnityEngine.SceneManagement;


namespace Sellswords
{
    public class InventoryScene : BaseMenu///скрипт должен быть на конвосе
    {
        //public Transform _slotBox;
        [SerializeField] private GameObject _buttonsTower;// кнопки меню город,лагерь,магазин
        [SerializeField] private ButtonUi _shop; //кнопочка магазина
        [SerializeField] private ButtonUi _camp;
        [SerializeField] private ButtonUi _city;
        [SerializeField] private ButtonUi _towerMenu;
        TestInventory Inventory; // класс хранивший инвентарь и его количество Заменить
        private void Start()
        {
            Inventory = TestInventory.instance;
            SlotUI[] _slots = GetComponentsInChildren<SlotUI>(); //все слоты
            for (int i = 0; i < _slots.Length; i++)
            {
                if (i < Inventory.testItems.Count)/// распределяем объекты в слоты
                {
                    _slots[i].AddItem(Inventory.testItems[i]);
                }
            }
            _shop.GetControl.onClick.AddListener(delegate
            {
                //SceneManager.LoadScene(2);// вызов сцены(Меню Shop)
        });
            _city.GetControl.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(0);
            });
            _camp.GetControl.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(1);
            });
            _towerMenu.GetControl.onClick.AddListener(delegate
            {
                Show(_buttonsTower);/// показывает кнопочки ивенторя,магазина,лагеря
        });
        }
        public override void Hide(GameObject gameObject) /// метод скрытия  
        {
            if (!IsShow) return;
            gameObject.gameObject.SetActive(false);
            IsShow = false;
        }
        public override void Show(GameObject gameObject) // метод показа
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
