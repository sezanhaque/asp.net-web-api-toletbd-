using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToLetBdEntity;
using ToLetBdInterface;

namespace ToLetBdRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {

        ToLetBdDbContext context = new ToLetBdDbContext();

        public List<TEntity> GetAll()
        {
            return this.context.Set<TEntity>().ToList();
        }

        public TEntity Get(int id)
        {
            return this.context.Set<TEntity>().Find(id);
        }

        public int Insert(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
            return this.context.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            this.context.Entry<TEntity>(entity).State = EntityState.Modified;
            return this.context.SaveChanges();
        }

        public int Delete(int id)
        {

            TEntity entity=this.context.Set<TEntity>().Find(id);
            this.context.Set<TEntity>().Remove(entity);
            return this.context.SaveChanges();
        }
    }
}
