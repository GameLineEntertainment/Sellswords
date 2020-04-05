namespace Sellswords
{
    public interface IPoolObject<out T>
    {
        T Find(int id);
    }
}