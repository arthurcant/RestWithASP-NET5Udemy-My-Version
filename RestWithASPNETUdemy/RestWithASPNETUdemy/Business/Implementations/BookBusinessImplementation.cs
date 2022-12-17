using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly BookConverter _converter;
        public BookBusinessImplementation(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
            _converter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _bookRepository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _bookRepository.Delete(id);
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_bookRepository.FindAll());
        }

        public BookVO FindById(long id)
        {
            var bookEntity = _bookRepository.FindById(id);
            return _converter.Parse(bookEntity);
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _bookRepository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }
    }
}