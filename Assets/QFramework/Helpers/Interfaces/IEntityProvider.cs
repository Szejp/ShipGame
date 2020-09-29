using System.Collections.Generic;
using QFramework.GameModule.GameTools.Entities;

namespace QFramework.Helpers.Interfaces
{
    public interface IEntityProvider
    {
        Entity GetEntity();
        List<Entity> GetEntities();
    }
}