using UnityEngine;


namespace Sellswords
{
    public interface IBaseModel
    {
        #region Properties

        int Id { get; }
        Transform Transform { get; }

        #endregion
    }
}