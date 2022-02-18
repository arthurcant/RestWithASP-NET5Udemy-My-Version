using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Services.implementation
{
    public class PersonServiceImplementation : IPersonService
    {
        List<Person> people = new List<Person>();

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete()
        {
            
        }

        public List<Person> FindAll()
        {

            return people;
        }

        public Person FindById()
        {
            return new Person {
                id:,
                FirstName: "Arthur",
                LastName: "Cavalcante",
                Address: "Prazeres Pernambuco Brazil",
                Gender: ()

            }
        }

        public Person update(Person person)
        {
            throw new NotImplementedException();
        }
    }
}