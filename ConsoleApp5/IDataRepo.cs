using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ConsoleApp5
{
    public interface IDataRepo<T>
    {
        T Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        void Update(T item);
        void Delete(T id);
        void Add(T item);
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);

    }
}
