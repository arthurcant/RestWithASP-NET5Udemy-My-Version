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
        private volatile int count;

        Person IPersonService.Create(Person person)
        {
            return person;
        }

        void IPersonService.Delete(long id)
        {

        }

        List<Person> IPersonService.FindAll()
        {
            List<Person> peoples = new List<Person>();

            for(int i = 0; i < 8; i++){
                Person person = MockPerson(i);
                peoples.Add(person);
            }
            return peoples;
        }


        Person IPersonService.FindByID(long id)
        {
            return new Person 
            {
                id = IncrementAndGet(),
                FirstName = "Leandro",
                LastName = "Costa",
                Address = "Uberlandia - Minas Gerais - Brasil",
                Gender = "Male"
            };
        }

        Person IPersonService.Update(Person person)
        {
            throw new NotImplementedException();
        }
        private Person MockPerson(int i)
        {
            return new Person 
            {
                id = IncrementAndGet(),
                FirstName = "Person Name" + i,
                LastName = "Person LastName" + i,
                Address = "Some Address" + i,
                Gender = (i % 2 != 0) ?"Female":"Male"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
        
    }
}