namespace Sellswords
{
    public interface IPerson : IBaseModel
    {
        #region Fields

        State State { get; set; }

        #endregion
        
        
        #region Methods
        void SetStatus(Status status);

        #endregion
    }
}