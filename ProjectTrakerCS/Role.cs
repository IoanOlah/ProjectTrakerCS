using Csla;
using Csla.Serialization;
using System;
using System.Linq;

namespace ProjectTraker.Library
{
    [Serializable]
    public class Role : BusinessBase<Role>
    {
        private static PropertyInfo<int> IdProperty = RegisterProperty(new PropertyInfo<int>("Id"));
        private bool _idSet;
        public int Id
        {
            get
            {
                if(!_idSet)
                {
                    _idSet = true;
                    SetProperty(IdProperty, GetMax() + 1);
                }
                return GetProperty(IdProperty);
            }
            set
            {
                _idSet = true;
                SetProperty(IdProperty, value);
            }
        }

        private int GetMax()
        {
            Roles parent = (Roles).this.Parent;
            return parent.Max(r => r.Id);
        }
    }
}
