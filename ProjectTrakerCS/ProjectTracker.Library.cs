using System;
using System.Linq;
using Csla;
using Csla.Data;
using Csla.Rules;
using Csla.Security;
using Csla.Serialization;

namespace ProjectTraker.Library
{
    [Serializable()]
    public class Project : BusinessBase<Project>
    {
        #region Business Methods +++ Verified +++

        private static PropertyInfo<byte[]> TimeStampProperty = RegisterProperty<byte[]>(c => c.TimeStamp);
        private byte[] TimeStamp
        {
            get { return GetProperty(TimeStampProperty); }
            set { SetProperty(TimeStampProperty, value); }
        }

        private static PropertyInfo<Guid> IdProperty = RegisterProperty(new PropertyInfo<Guid>("Id"));
        [System.ComponentModel.DataObjectField(true,true)]
        public Guid Id
        {
            get { return GetProperty(IdProperty); }
        }

        private static PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name, "Project Name");
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        private static PropertyInfo<string> DescriptionProperty = RegisterProperty(new PropertyInfo<string>("Description"));
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        private static PropertyInfo<SmartDate> StartedProperty = RegisterProperty(new PropertyInfo<SmartDate>("Started"));
        public string Started
        {
            get { return GetPropertyConvert<SmartDate, string>(StartedProperty); }
            set { SetPropertyConvert<SmartDate, string>(StartedProperty, value); }
        }

        private static PropertyInfo<SmartDate> EndedProperty = RegisterProperty<SmartDate>(p => p.Ended, null, new SmartDate(SmartDate.EmptyValue.MaxDate));
        public string Ended
        {
            get { return GetPropertyConvert<SmartDate, string>(EndedProperty); }
            set { SetPropertyConvert<SmartDate, string>(EndedProperty, value); }
        }

        private static PropertyInfo<ProjectResources> ResourcesProperty = RegisterProperty(new PropertyInfo<ProjectResources>("Resources"));
        public ProjectResources Resources
        {
            get
            {
                if (!(FieldManager.FieldExists(ResourcesProperty)))
                    LoadProperty(
                        ResourcesProperty,
                        ProjectResources.NewProjectResources());
                return GetProperty(ResourcesProperty);
            }
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        #endregion
        #region Business and Validation Rules +++ Verified +++

        protected override void AddBusinessRules()
        {
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(NameProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(NameProperty, 50));
            
            BusinessRules.AddRule(new StartDateGTEndDate { PrimaryProperty = StartedProperty, AffectedProperties = { EndedProperty } });
            BusinessRules.AddRule(new StartDateGTEndDate { PrimaryProperty = EndedProperty, AffectedProperties = { StartedProperty } });

            BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.WriteProperty, NameProperty, "ProjectManager"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.WriteProperty, StartedProperty, "ProjectManager"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.WriteProperty, EndedProperty, "ProjectManager"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.WriteProperty, DescriptionProperty, "ProjectManager"));
        }
        
        protected static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(Project), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, "ProjectManager"));
            Csla.Rules.BusinessRules.AddRule(typeof(Project), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, "ProjectManager"));
            Csla.Rules.BusinessRules.AddRule(typeof(Project), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, "ProjectManager"));
        }

        private class StartDateGTEndDate : Csla.Rules.BusinessRule
        {
            protected override void Execute(RuleContext context)
            {
                var target = (Project)context.Target;
                if (target.ReadProperty(StartedProperty) > target.ReadProperty(EndedProperty))
                    context.AddErrorResult("Start date can't be after end date");
            }
        }

        #endregion
        #region Authorization Rules
        #endregion
        #region Factory Methods
        #endregion
        #region Data Access
        #endregion
        #region Exists
        #endregion
    }
}
