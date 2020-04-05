
namespace Sellswords
{
    public interface ISpell : IBaseModel
    {
        bool Use<T>(T targets);
    }
}