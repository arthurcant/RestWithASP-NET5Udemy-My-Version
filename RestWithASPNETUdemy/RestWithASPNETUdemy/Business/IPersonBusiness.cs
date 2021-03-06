using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business
{
    public interface IPersonBusiness
    {
        Person Create(Person person);
        Person FindByID(long id);
        List<Person> FindAll();
        Person Update(Person person);
        Person Disable(long id);
        void Delete(long id);
        List<Person> FindByName(string firstName, string secondName);
        
    }
}
