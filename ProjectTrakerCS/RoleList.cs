using Csla;
using Csla.Data;
using System;
using System.Linq;
using Csla.Serialization;

namespace ProjectTraker.Library
{
    [Serializable()]
    public class RoleList:NameValueListBase<int,string>
    {
        #region Business Methods +++ Valid +++
        
        public static int DefaultRole()
        {
            RoleList list = GetList();
            if (list.Count > 0)
                return list.Items[0].Key;
            else
                throw new NullReferenceException("No roles available. Default role can not be returned.");
        }

        #endregion

        #region Static Cache  +++ Valid +++

        private static RoleList _list;

        public static void InvalidateCache()
        {
            _list = null;
        }

        #endregion

        #region Factory Methods +++ Valid +++

        public static RoleList GetList()
        {
            if (_list == null)
                _list = DataPortal.Fetch<RoleList>();
            return _list;
        }

        private RoleList()
        { }

        #endregion

    }
}
