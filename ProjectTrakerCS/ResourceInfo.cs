using Csla;
using System;
using Csla.Serialization;

namespace ProjectTraker.Library
{
    [Serializable()]
    public class ResourceInfo:ReadOnlyBase<ResourceInfo>
    {
        #region Business Methods

        /*implementation from book*/
        /*
        private int _id;
        public int Id
        {
            get { return _id; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
        }

        public override string ToString()
        {
            return _name;
        }

        internal ResourceInfo(int id, string lastname, string firstname)
        {
            _id = id;
            _name = string.Format("{0}, {1}", lastname, firstname);
        }
        */

        /* implementation from website download */
        private static PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        private static PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        public override string ToString()
        {
            return Name;
        }

        internal ResourceInfo(int id, string lastname, string firstname)
        {
            Id = id;
            Name = string.Format("{0}, {1}", lastname, firstname);
        }
        #endregion
    }
}
