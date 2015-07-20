using Fakultet_IS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fakultet_IS.DAL
{
    public class FakultetRepository<TEntity> : IFakultetRepository<TEntity> where TEntity : class 
    {
        internal FakultetEntities context;
        internal DbSet<TEntity> dbSet;

        public FakultetRepository(FakultetEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetEntities()
        {
            return dbSet.ToList();
        }

        public virtual TEntity GetEntityById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void InsertEntity(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void DeleteEntity(object id)
        {
            TEntity entity = dbSet.Find(id);
            DeleteEntity(entity);
        }

        public virtual void DeleteEntity(TEntity entity)
        {
            if(context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void UpdateEntity(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}