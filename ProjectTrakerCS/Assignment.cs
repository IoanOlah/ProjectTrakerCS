using System;
using Csla;
using Csla.Data;
using Csla.Rules;

namespace ProjectTraker.Library
{
    internal interface IHoldRoles
    {
        int Role { get; set; }
    }

    internal static class Assignment
    {
        #region  Business Methods +++ Validated +++

        public static System.DateTime GetDefaultAssignedDate()
        {
            return System.DateTime.Today;
        }

        #endregion

        #region Validation Rules +++ Validated +++

        /* implementation from business objects 2008 book 
        public static bool ValidRole<T>(T target, RuleArg e) where T:IHoldRoles
        {
            int role = target.Role;
            if (RoleList.GetList().ContainsKey(role))
            {
                return true;
            }
            else
            {
                e.Description = "Role must be in RoleList";
                return false;
            }
        }
        */
        /* implementation from data mart */
        public class ValidRole : BusinessRule
        {
            protected override void Execute(RuleContext context)
            {
                var target = (IHoldRoles)context.Target;
                int role = target.Role;

                if (!RoleList.GetList().ContainsKey(role))
                    context.AddErrorResult("Role must be in RoleList");
            }
        }
        
        #endregion

        #region Data Access
        #endregion
    }
}
