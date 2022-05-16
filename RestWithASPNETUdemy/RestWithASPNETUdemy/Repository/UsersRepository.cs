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
        
        public MySQLContext _Context;
        
        public UsersRepository(MySQLContext mySQLContext)
        {
            _Context = mySQLContext;
        }

        
        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputerHash(user.Password, new SHA256CryptoServiceProvider());
            return _Context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }
        public User ValidateCredentials(string UserName)
        {
            return _Context.Users.FirstOrDefault(u => (u.UserName == UserName));
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

            var element = _Context.Users.SingleOrDefault((t) => t.Id.Equals(user.Id));

            if (element != null)
            {
                try
                {
                    _Context.Entry(element).CurrentValues.SetValues(user);
                    _Context.SaveChanges();
                    return element;

                }
                catch (Exception)
                {
                    throw;
                }
            }

            return element;
        }


        public bool Exist(long id) => _Context.Users.Any(p => p.Id.Equals(id));

        public User RefreshUserInfo(User user)
        {
            if (!_Context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var element = _Context.Users.SingleOrDefault((t) => t.Id.Equals(user.Id));

            if (element != null)
            {
                try
                {
                    _Context.Entry(element).CurrentValues.SetValues(user);
                    _Context.SaveChanges();
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
