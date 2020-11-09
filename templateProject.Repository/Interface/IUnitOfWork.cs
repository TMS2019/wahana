using System;

namespace templateProject.Repository.Interface
{
    interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        void Dispose();
    }
}
