using System;
using System.Linq;

namespace templateProject.Repository.Interface
{
    interface IGenericRepository<T>
    {
        void Insert(T item);
        void Update(T item);
        void Delete(T item);

        IQueryable<T> SelectAll();
        T SelectOne(int id);
        T SelectOne(string id);
    }
}
