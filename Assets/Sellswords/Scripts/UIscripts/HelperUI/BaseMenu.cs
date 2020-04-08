using Object = UnityEngine.Object;
using UnityEngine;
using System;


namespace Sellswords
{
    public abstract class BaseMenu : MonoBehaviour// базовый класс для менюшек
    {
        protected bool IsShow { get; set; }
        public abstract void Hide(GameObject gameObject);// метод скрытия
        public abstract void Show(GameObject gameObject);// метод показа
    }
}