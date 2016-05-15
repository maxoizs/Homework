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

        public Repository()
        {
            Database = new DbFactory().Create();
            DbSet = Database.Set<T>();
        }

        /// <summary>
        /// Create or Update T
        /// </summary>
        /// <param name="items"></param>
        public void Save(params T[] items)
        {
            DbSet.AddOrUpdate(items);
            Database.SaveChanges();
        }

        public void Delete(T item)
        {
            DbSet.Remove(item);
            Database.SaveChanges();
        }

        public IEnumerable<T> Get(System.Func<T, bool> selector)
        {
            return Database.Set<T>().Where(selector);
        }
    }
}
