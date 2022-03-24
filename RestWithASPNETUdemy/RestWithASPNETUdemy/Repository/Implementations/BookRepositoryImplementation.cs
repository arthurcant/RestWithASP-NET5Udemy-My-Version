using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {

        private MySQLContext _contextBook;
        public BookRepositoryImplementation(MySQLContext contextBook)
        {
            _contextBook = contextBook;
        }

        public Book Create(Book book)
        {
            try{
                _contextBook.Add(book);
                _contextBook.SaveChanges();
            }catch(Exception){
                throw;
            }
            return book;
        }

        public void Delete(long id)
        {

            var bookElement = FindingId(id);
            if(bookElement == null) return;

            _contextBook.Remove(bookElement);
            _contextBook.SaveChanges();
            
        }

        public List<Book> FindAll()
        {
            return _contextBook.books.ToList();
        }

        public Book FindById(long id)
        {
            var bookElement = FindingId(id);
            if(bookElement == null) return null;

            return bookElement;
        }

        private Book FindingId(long id)
        {
            var bookElement = _contextBook.books.SingleOrDefault((book) => book.Id.Equals(id));
            return bookElement;
        }

        public Book Update(Book book)
        {
            try{
                var index = _contextBook.books.SingleOrDefault((book) => book.Id.Equals(book.Id));
                if(index == null) return null;

                _contextBook.Entry(index).CurrentValues.SetValues(book);
                _contextBook.SaveChanges();


            }catch(Exception){
                throw;
            }


            return book;
        }
    }
}