using Csla;
using System;
using Csla.Serialization;

namespace ProjectTraker.Library
{
    [Serializable()]
    public class ProjectResources : BusinessListBase<ProjectResources, ProjectResource>
    {
        #region  Business Methods +++ Valid +++


        public ProjectResource GetItem(int resourceId)
        {
            foreach (ProjectResource res in this)
                if (res.ResourceId == resourceId)
                    return res;
            return null;
        }

        public void Assign(int resourceId)
        {
            if (!Contains(resourceId))
            {
                ProjectResource resource = ProjectResource.NewProjectResource(resourceId);
                this.Add(resource);
            }
            else
                throw new InvalidOperationException("Resource already assigned to project");
        }

        public void Remove(int resourceId)
        {
            foreach(ProjectResource res in this)
            {
                if(res.ResourceId==resourceId)
                {
                    Remove(res);
                    break;
                }
            }
        }

        public bool Contains(int resourceId)
        {
            foreach (ProjectResource res in this)
                if (res.ResourceId == resourceId)
                    return true;
            return false;
        }

        public bool ContainsDeleted(int resourceId)
        {
            foreach (ProjectResource res in DeletedList)
                if (res.ResourceId == resourceId)
                    return true;
            return false;
        }


        #endregion


        internal static ProjectResources NewProjectResources()
        {
            return DataPortal.CreateChild<ProjectResources>();
        }

        #region Factory Methods

        #endregion

        #region Data Access

        #endregion
    }
}
