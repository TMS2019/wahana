using System;
using System.Linq;
using System.Data.Entity;

using templateProject.Repository.Interface;

namespace templateProject.Repository.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public DbContext Db;
        public DbSet<T> DbSet;

        #region Constructors
        public GenericRepository(DbContext ctx)
        {
            this.Db = ctx;
            this.DbSet = Db.Set<T>();
        }

        public GenericRepository()
        {

        }
        #endregion

        public void SaveChanges()
        {
            try
            {
                Db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public void Insert(T item)
        {
            DbSet.Add(item);
            SaveChanges();
        }

        public void Update(T item)
        {
            Db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            SaveChanges();
        }

        public void Delete(T item)
        {
            Db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            SaveChanges();
        }

        public void tested(T items)
        {
            Db.Entry(items).State = System.Data.Entity.EntityState.Deleted;
            SaveChanges();
        }

        public IQueryable<T> SelectAll()
        {
            return DbSet;
        }

        public T SelectOne(int id)
        {
            return DbSet.Find(id);
        }

        public T SelectOne(string id)
        {
            return DbSet.Find(id);
        }
    }
}
