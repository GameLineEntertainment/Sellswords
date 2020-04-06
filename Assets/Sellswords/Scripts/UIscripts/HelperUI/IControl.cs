namespace Sellswords
{
    public interface IControl
    {
        UnityEngine.GameObject Instance { get; }//ссылка на эллемент окна 
        UnityEngine.UI.Selectable Control { get; }///ссылка на интерактивный объект
    }
    public interface IControlText : IControl
    {
        UnityEngine.UI.Text GetText { get; } /// Текстовая кнопка
    }
    public interface IControlImage : IControl
    {
        UnityEngine.UI.Image GetImage { get; }//Кнопочка с картинкой    
    }
}
