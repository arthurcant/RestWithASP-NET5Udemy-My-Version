using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Base;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository.Implementations
{
    public class GenericRepository<T> : IRepository<T> where T: BaseEntity // Essa keyword where vai funcionar como um filtro para dizer que a entidade T vai herda a classe BaseEntity.
    {
        private MySQLContext _context;

        private DbSet<T> dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>(); // Está ajustando que o ajuste de qual tipo de DbSet vai ser 
                                         // usado, que vai ser dinamicamente visto em tempo de execução
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return item;
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault((t) => t.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }

            }

        }


        public T FindById(long id)
        {
            return dataset.SingleOrDefault((t) => t.Id.Equals(id));
        }

        public T Update(T item)
        {
            var element = dataset.SingleOrDefault((t) => t.Id.Equals(item.Id));
            if (element != null)
            {
                try
                {
                    _context.Entry(element).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return item;

                }
                catch (Exception)
                {
                    throw;
                }
            }

            return item;
        }

        public bool Exist(long id) => dataset.Any(p => p.Id.Equals(id));

    }
}