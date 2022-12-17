using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithASPNETUdemy.Data.Converter.Contract;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Data.Converter.Implementations
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public BookVO Parse(Book origin)
        {
            if(origin == null) return null;

            return new BookVO {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.launchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }


        public Book Parse(BookVO origin)
        {
            if(origin == null) return null;

            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                launchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public List<Book> Parse(List<BookVO> origin)
        {
            if(origin == null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }
 
        public List<BookVO> Parse(List<Book> origin)
        {
            if(origin == null) return null;

            return origin.Select((item) => Parse(item)).ToList();
        }
 
    }
}