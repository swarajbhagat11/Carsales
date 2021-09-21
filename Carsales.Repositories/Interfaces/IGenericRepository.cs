using System;
using System.Collections.Generic;

namespace Carsales.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        IEnumerable<T> Find(Func<T, bool> filter = null);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}
