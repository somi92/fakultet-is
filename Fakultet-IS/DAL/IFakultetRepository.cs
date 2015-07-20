using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakultet_IS.DAL
{
    public interface IFakultetRepository<TEntity>
    {
        IEnumerable<TEntity> GetEntities();
        TEntity GetEntityById(object id);
        void InsertEntity(TEntity entity);
        void DeleteEntity(object id);
        void DeleteEntity(TEntity entity);
        void UpdateEntity(TEntity entity);
    }
}
