using System;
using Sellswords;

namespace Sellswords
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class ShowInInspectorByStatusAttribute : Attribute
    {
        public StatusType StatusType { get; set; }

        public ShowInInspectorByStatusAttribute(StatusType statusType)
        {
            StatusType = statusType;
        }
    }
}