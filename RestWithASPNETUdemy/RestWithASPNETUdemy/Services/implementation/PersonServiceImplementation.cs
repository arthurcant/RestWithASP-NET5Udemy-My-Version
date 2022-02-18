using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Expressions;
using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Services.implementation
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;
        private bool listImplement = false;
        List<Person> people = new List<Person>();

        public Person Create(Person person)
        {
            person.Id = IncrementAndGet();
            people.Add(person);
            return person;
        }

        public void Delete()
        {
            
        }

        public List<Person> FindAll()
        {
            if(listImplement == false){
                for(int i = 0; i < 11; i++){
                    Person person = mockPerson(i);
                    people.Add(person);
                }
                listImplement = true;
            }
            return people;
        }

        private Person mockPerson(int i)
        {
            return new Person 
            {
                Id = IncrementAndGet(),
                FirstName = "Arthur" + i,
                LastName = "Cavalcante" + i,
                Address = "Prazeres Pernambuco Brazil" + i,
                Gender = "Masculino" + i
            };
        }

        public Person FindById(long id)
        {
            // long saveNum = id;
            // int x = Convert.ToInt32(id);
            bool verifying = false;
            int saveNum = unchecked((int) id);

            verifying = verifyingPerson(id);

            if(verifying == true) {
                return people[saveNum];
            }
            return people[saveNum];
            
        }

        private bool verifyingPerson(long saveNum)
        {
            bool x = false;

            if(people.Any(l => l.Id == saveNum)) {
                x = true;
            }

            return x;
        }

        public Person update(Person person, int id)
        {
            people[id].FirstName = person.FirstName;
            people[id].LastName = person.LastName;
            people[id].Address = person.Address;
            people[id].Gender = person.Gender;

            return people[id];
        }
        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

    }
}