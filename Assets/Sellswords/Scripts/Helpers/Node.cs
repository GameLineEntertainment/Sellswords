namespace Sellswords
{
    public class Node<T>
    {
        #region Properties

        public T Data { get; set; }
        public Node<T> Next { get; set; }

        #endregion

        #region ClassLifeCycles

        public Node(T data)
        {
            Data = data;
        }

        #endregion
    }
}