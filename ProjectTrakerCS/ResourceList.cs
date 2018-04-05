using Csla;
using Csla.Data;
using System;
using System.Linq;
using Csla.Serialization;

namespace ProjectTraker.Library
{
    [Serializable()]
    public class ResourceList:ReadOnlyListBase<ResourceList, ResourceInfo>
    {
    }
}
