using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository.Implementations;
using System;
using System.Linq;

namespace RestWithASPNETUdemy.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context) { }
        
        public Person Disable(long id)
        {

            if (!_context.People.Any(p => p.Id.Equals(id)));

            var user = _context.People.SingleOrDefault(p => p.Id.Equals(id));

            if(user != null)
            {
                user.Enable = false;

                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();

                }
                catch (Exception)
                {

                    throw;
                }

            }

            return user;
        }
    }
}
