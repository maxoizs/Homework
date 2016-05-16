using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace PersistXML.Repositories
{
    public class Repository<T> where T : class
    {
        protected readonly DbSet<T> DbSet;
        protected readonly Database Database;

        public Repository(DbFactory dbContextFactory)
        {
            Database = dbContextFactory.Create();
            DbSet = Database.Set<T>();
        }

        /// <summary>
        /// Create or Update T
        /// </summary>
        public virtual void Save(params T[] items)
        {
            //Not a wise option but a quick one.
            DbSet.AddOrUpdate(items);
            Database.SaveChanges();
        }

        public virtual void Delete(T item)
        {
            DbSet.Remove(item);
            Database.SaveChanges();
        }

        public virtual IEnumerable<T> Get(System.Func<T, bool> selector)
        {
            return DbSet.Where(selector);
        }
    }
}
