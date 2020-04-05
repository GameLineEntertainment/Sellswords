namespace Sellswords
{
    public interface IStatus
    {
        #region Fields

        float Time { get; set; }

        #endregion


        #region Methods

        void Use();

        #endregion
    }
}