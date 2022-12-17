using System.Collections.Generic;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Base;

namespace RestWithASPNETUdemy.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);

        List<T> FindWithPagedSearch(string query);
        int GetCount(string query); // Para manda a quantidade de registros que foram achados para o client 
                                    // informação interreçante para o front-end trabalha.

    }
}