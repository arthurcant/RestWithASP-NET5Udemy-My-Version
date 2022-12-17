using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Utils;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;

namespace RestWithASPNETUdemy.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        // Counter responsible for generating a fake ID
        // since we are not accessing any database

        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;
        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        // Method responsible for creating a new person.
        // If we had a database this would be the time to persist the data
        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        // Method responsible for deleting a person from an ID
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }

        // Method responsible for returning all people,
        // again this information is mocks
        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrEmpty(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc"; // resiliencia da aplicação caso o usuário 
                                                                                                                 // mande algo diferente de asc e desc.
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1 = 1"; // Where 1 = 1 é para garanti que a query seja executada mesmo se
                                                                  // o usuário não passou as variaveis sort, size, offset;
            if (!string.IsNullOrEmpty(name)) query += query + $" and p.first_name like '%{name}%' ";
            query += $" order by p.first_name {sort} limit {size} offset {offset}";

            string countQuery = @"select count(*) from person p where 1 = 1";
            if (!string.IsNullOrEmpty(name)) countQuery = countQuery + $" and p.first_name like '%{name}%' ";

            var persons = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO> {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }


        // Method responsible for returning a person
        // as we have not accessed any database we are returning a mock
        public PersonVO FindByID(long id)
        {
            var personEntity = _repository.FindById(id);
            return _converter.Parse(personEntity);
        }

        public List<PersonVO> FindByName(string firstName, string secondName)
        {
            return _converter.Parse(_repository.FindByName(firstName, secondName));

        }

        // Method responsible for updating a person for
        // being mock we return the same information passed
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }
    }
}
