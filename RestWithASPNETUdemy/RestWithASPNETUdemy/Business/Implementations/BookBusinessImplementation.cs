using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
namespace RestWithASPNETUdemy.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private IBookBusiness _iBookRepository;
        public BookBusinessImplementation(IBookBusiness iBook)
        {
            _iBookRepository = iBook;
        }

        public Book Create(Book book)
        {
            return _iBookRepository.Create(book);
        }

        public void Delete(long id)
        {
            _iBookRepository.Delete(id);
        }

        public List<Book> FindAll()
        {
          return _iBookRepository.FindAll();
        }

        public Book FindById(long id)
        {
            return _iBookRepository.FindById(id);
        }

        public Book Update(Book book)
        {
            return _iBookRepository.Update(book);
        }
    }
}