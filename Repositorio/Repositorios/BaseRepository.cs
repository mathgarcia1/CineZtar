using Microsoft.EntityFrameworkCore;
using Repositorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repositorio.Repositorios
{
    public class BaseRepository<T>
         : IDisposable, IBaseRepository<T> where T : class
    {

        private DB_IngressosContext _context;
        private DbSet<T> _table;

        public void SetContext(DB_IngressosContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        // Resto do código...

        private DbSet<T> Table
        {
            get { return _table; }
        }




        public T get(int id)
        {
            using (_context = new DB_IngressosContext())
            {
                return _context.Set<T>().Find(id);
            }

        }

        public List<T> getAll()
        {
            using (_context = new DB_IngressosContext())
            {
                return _context.Set<T>().ToList();
            }

        }

        public void add(T item)
        {
            using (_context = new DB_IngressosContext())
            {
                _context.Set<T>().Add(item);
                _context.SaveChanges();
            }


        }

        public void delete(T item)
        {
            using (_context = new DB_IngressosContext())
            {
                //_context.Set<T>().Remove(item);
                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();
            }


        }

        public void edit(T item)
        {
            using (_context = new DB_IngressosContext())
            {
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
            }

        }



        public void Dispose()
        {
            using (_context = new DB_IngressosContext())
            {
                _context.Dispose();
            }

        }

        public virtual T Recuperar(Expression<Func<T, bool>> expressao)
        {

            return this.Table
                .Where(expressao)
                .SingleOrDefault();

        }

        public virtual List<T> Listar(Expression<Func<T, bool>> expressao)
        {

            return this.Table
                .Where(expressao)
                .ToList();

        }
    }



}
