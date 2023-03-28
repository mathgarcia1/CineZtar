using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Repositorios
{
    public interface IBaseRepository<T> where T : class
    {
        T get(int id);
        List<T> getAll();
        void add(T item);
        void delete(T item);
        void edit(T item);
    }
}
