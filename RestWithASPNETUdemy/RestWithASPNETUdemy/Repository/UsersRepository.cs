using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNETUdemy.Repository
{
    public class UsersRepository : IUsersRepository
    {
        
        public MySQLContext _context;
        
        public UsersRepository(MySQLContext mySQLContext)
        {
            _context = mySQLContext;
        }

        
        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputerHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }
        public User ValidateCredentials(string UserName)
        {
            return _context.Users.FirstOrDefault(u => (u.UserName == UserName));
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => (u.UserName == userName));
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        // embarrassed the password in SHA256.
        private string ComputerHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input); // this line Byte[] inputBytes receive the password and return input to byte array.
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes); //  Converts base data types to an array of bytes
          
            //Console.WriteLine($"{hashedBytes}");
            //Console.WriteLine($"{x}");
            //var x = BitConverter.ToString(hashedBytes);
        }

        public User Update(User user)
        {
            if(!Exist(user.Id)) return null;

            var element = _context.Users.SingleOrDefault((t) => t.Id.Equals(user.Id));

            if (element != null)
            {
                try
                {
                    _context.Entry(element).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return element;

                }
                catch (Exception)
                {
                    throw;
                }
            }

            return element;
        }


        public bool Exist(long id) => _context.Users.Any(p => p.Id.Equals(id));

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var element = _context.Users.SingleOrDefault((t) => t.Id.Equals(user.Id));

            if (element != null)
            {
                try
                {
                    _context.Entry(element).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return user;

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
