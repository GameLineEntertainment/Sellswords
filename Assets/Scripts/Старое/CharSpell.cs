using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSpell : MonoBehaviour
{
    /*
    Рыцарь - +1 жизнь
    Валькирия - Больше опыта за убитых ею 
    Водяная баба - Энергия быстрее восстанавливается ~25%
    Энчантер - оглушает ударом
    \Гладиатор - Больше золота с тех кого он убил\
    Огненая разбойница - Убийства этим героем восстанавливают энергию
    Некромантша - Больше шанс выпадения предметов-частей тел врагов(головы и тд) (~20-33% всего лута)
    Пурпурный маг - Игрок получает больше опыта на 10%
    Большой воин - Враги которые появляются когда он впереди ходят медленнее
    Рэйвен - Больше дорогого лута 20-33% от всего лута
    */

    [SerializeField]
    [Tooltip("Ведётся ли обработка через снаряд?")]
    bool SpellToBullet;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Инициализация, что делает этот скрипт
    /// </summary>
    /// <returns></returns>
    public bool Initialize(MiniArcher my_char)
    {
        if (SpellToBullet)
            return true;

        return false;
    }
}
