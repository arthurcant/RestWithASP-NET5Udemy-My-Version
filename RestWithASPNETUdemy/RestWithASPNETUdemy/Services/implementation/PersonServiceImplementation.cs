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
        public volatile int count;
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

        public Person FindById(long id)
        {
            return new Person 
            {
                Id: IncrementAndGet(id),
                FirstName: "Arthur",
                LastName: "Cavalcante",
                Address: "Prazeres Pernambuco Brazil",
                Gender: "Masculino"
            };
        }

        private object IncrementAndGet(int count)
        {
            Interlocked.Increment(out count);
        }

        public Person update(Person person)
        {
            throw new NotImplementedException();
        }
    }
}