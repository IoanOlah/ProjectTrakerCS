using Csla;
using System;
using Csla.Serialization;

namespace ProjectTraker.Library
{
    [Serializable]
    public class ProjectResource : BusinessBase<ProjectResource>, IHoldRoles
    {
        #region Business Methods +++ Verified +++

        private static PropertyInfo<byte[]> TimeStampProperty = RegisterProperty<byte[]>(c => c.TimeStamp);
        private byte[] TimeStamp
        {
            get { return GetProperty(TimeStampProperty); }
            set { SetProperty(TimeStampProperty, value); }
        }

        private static PropertyInfo<int> ResourceIdProperty = RegisterProperty<int>(p => p.ResourceId, "Resource id");
        public int ResourceId
        {
            get { return GetProperty(ResourceIdProperty); }
        }

        private static PropertyInfo<string> FirstNameProperty = RegisterProperty(new PropertyInfo<string>("First Name"));
        public string FirstName
        {
            get { return GetProperty(FirstNameProperty); }
        }

        private static PropertyInfo<string> LastNameProperty = RegisterProperty(new PropertyInfo<string>("Last Name"));
        public string LastName
        {
            get { return GetProperty(LastNameProperty); }
        }

        public string FullName
        {
            get { return string.Format("{0}, {1}", LastName, FirstName); }
        }

        private static PropertyInfo<SmartDate> AssignedProperty = RegisterProperty(new PropertyInfo<SmartDate>("Date assigned"));
        public string Assigned
        {
            get { return GetPropertyConvert<SmartDate, string>(AssignedProperty); }
        }

        private static PropertyInfo<int> RoleProperty = RegisterProperty<int>(p => p.Role, "Role assigned");
        public int Role
        {
            get { return GetProperty(RoleProperty); }
            set { SetProperty(RoleProperty, value); }
        }

        //public static readonly MethodInfo GetResourceMethod = RegisterMethod(typeof(ProjectResource), "GetResource");
        //public Resource GetResource()
        //{
        //  CanExecuteMethod(GetResourceMethod, true);
        //  return Resource.GetResource(GetProperty(ResourceIdProperty));
        //}

        public override string ToString()
        {
            return ResourceId.ToString();
        }

        #endregion

        #region Business and Validation Rules  +++ Validated +++

        protected override void AddBusinessRules()
        {
            BusinessRules.AddRule(new Assignment.ValidRole { PrimaryProperty = RoleProperty });
            BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.WriteProperty, RoleProperty, "ProjectManager"));
        }

        #endregion

        #region NewProjectResource +++ Valid +++

        internal static ProjectResource NewProjectResource(int resourceId)
        {
            return DataPortal.CreateChild<ProjectResource>(resourceId, RoleList.DefaultRole());
        }

        protected override void Child_Create()
        {
            LoadProperty(AssignedProperty, new SmartDate(System.DateTime.Today));
        }

        #endregion

        #region Factory Methods
        #endregion

        #region Data Access
        #endregion

    }
}
