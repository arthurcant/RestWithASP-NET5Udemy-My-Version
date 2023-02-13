using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Utils;
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

        public PagedSearchVO<BookVO> FindWithPagedSearch(string title, string sortDirection, int pageSize, int page) 
        {
            var sort = (!string.IsNullOrEmpty(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
            
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from books b where 1 = 1";

            if (!string.IsNullOrEmpty(title)) query += $"and b.title like '%{title}%' ";
            query += $" order by b.title {sort} limit {size} offset {offset}";

            string countQuery = @"select count(*) from books b where 1 = 1";
            if (!string.IsNullOrEmpty(title)) countQuery = countQuery + $" and b.title like '%{title}%' ";

            var books = _bookRepository.FindWithPagedSearch(query);
            int totalResults = _bookRepository.GetCount(countQuery);

            return new PagedSearchVO<BookVO>
            {
                CurrentPage = page,
                List = _converter.Parse(books),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
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