using System;
using System.Collections.Generic;

namespace WpfApplicationPractice.Models
{
    public interface IData<T>
    {
        List<T> GetAll();
        List<T> Get(Func<T, bool> predicate);
        T GetFirst(Func<T, bool> predicate);
        void AddOrUpdate(T entity);
        bool Exists(T entity);
        void Remove(T entity);
    }
}