using System.Collections;
using System.Collections.Generic;
using OldSellswords;
using UnityEngine;

public class Inventory : MonoBehaviour {

    List<Item> items;

    // масив валют для скрытия в меню
    public List<LootItem> currencies = new List<LootItem>();

    // визуализация ячеек
    List<RectTransform> cells = new List<RectTransform>();
    Rect cellRect;
    Vector2 calculationOffset;                  // выравнивание по левому верзнему краю
    float widthOffset;                          // централизация ячеек по ширине
    int maxColumn;                              // количество умещающихся в cellsRect колонок с предметами
    InventoryCell focusedCell;                  // выбранный предмет
    public RectTransform cellsRect;             // поле в котором будут выводиться предметы
    public UnityEngine.UI.Text desctiptionText; // описание выбранного предмета
    public GameObject cellPrefab;
    public Vector2 offsetCellPosition;

    // Use this for initialization 
    void Start() {
        items = Variables.playerItems.items;
        // переменные для выравнивания сетки
        cellRect = cellPrefab.GetComponent<RectTransform>().rect;
        maxColumn = (int) (cellsRect.rect.width / (cellRect.width + offsetCellPosition.x)) + 1;
        widthOffset = (cellsRect.rect.width - maxColumn * (cellRect.width + offsetCellPosition.x) + 1) / 2;
        calculationOffset = new Vector2(-cellsRect.rect.width + cellRect.width, cellsRect.rect.height - cellRect.height) / 2;
        // удаляем старые обьекты деток
        for (int i = cellsRect.childCount; i > 0; i--) Destroy(cellsRect.GetChild(i - 1).gameObject);
        cells.Clear();
    }

    private void Update() {
        if (items == null) return;
        //bool updateItems = false;
        //// добавление новых клеток
        //for (int i = 0; i < items.Count; i++) {
        //    if (!currencies.Contains(items[i].loot)) {
        //        bool itemInCells = false;
        //        for (int j = 0; j < cells.Count; j++) {
        //            if (cells[j].GetComponent<InventoryCell>().item == items[i]) {
        //                itemInCells = true;
        //                break;
        //            }
        //        }
        //        if (!itemInCells) {
        //            GameObject go = Instantiate(cellPrefab, cellsRect.transform);
        //            go.GetComponent<InventoryCell>().setItem(items[i], this);
        //            cells.Add(go.GetComponent<RectTransform>());
        //            updateItems = true;
        //        }
        //    }
        //}
        //// очистка удалённых клеток
        //for (int i = 0; i < cells.Count; i++) {
        //    if (!items.Contains(cells[i].GetComponent<InventoryCell>().item)) {
        //        Destroy(cells[i].gameObject);
        //        if (cells[i].GetComponent<InventoryCell>() == focusedCell) desctiptionText.text = "";
        //        cells.Remove(cells[i]);
        //        updateItems = true;
        //    }
        //}
        //// отрисовка инвенторя
        //if (updateItems) {
        //    drawCells();
        //}
    }

    // вырисовывание сетки
    void drawCells() {
        int currentCol = 0;
        int currentRow = 0;
        for (int i = 0; i < cells.Count; i++) {
            cells[i].localPosition = calculationOffset + new Vector2(currentCol * (cellRect.width + offsetCellPosition.x) + widthOffset, -currentRow * (cellRect.height + offsetCellPosition.y));
            if (++currentCol >= maxColumn) {
                currentCol = 0;
                currentRow++;
            }
        }
    }

    // при нажатии на ячейку она попадает в фокус
    public void cellOnFocus(InventoryCell cell) {
        desctiptionText.text = cell.item.loot.description;
        focusedCell = cell;
    }

    // покупка снимает фокус и очищает ячейук
    public void sellFocusedCell() {
        //if (focusedCell != null && focusedCell.item.loot.price.loot != null && focusedCell.item.loot.price.count > 0) {
        //    // продаём одну лутинку
        //    LootItem.Stack sellOne = focusedCell.item.copy();
        //    sellOne.count = 1;
        //    Variables.playerItems.makeOffer(sellOne, focusedCell.item.loot.price);
        //}
    }
}
