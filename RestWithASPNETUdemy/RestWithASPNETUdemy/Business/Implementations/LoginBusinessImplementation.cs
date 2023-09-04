using RestWithASPNETUdemy.Configurations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNETUdemy.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private IUsersRepository _repositoryUser;
        private IRepository<User> _repository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUsersRepository repository, ITokenService tokenService, IRepository<User> repository2)
        {
            _configuration = configuration;
            _repositoryUser = repository;
            _tokenService = tokenService;
            _repository = repository2;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            var user = _repositoryUser.ValidateCredentials(userCredentials);

            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            DateTime getNow = DateTime.Now;
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            _repositoryUser.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                   true,
                   createDate.ToString(DATE_FORMAT),
                   expirationDate.ToString(DATE_FORMAT),
                   accessToken,
                   refreshToken
                );
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var username = principal.Identity.Name;

            var user = _repositoryUser.ValidateCredentials(username);

            if (user == null ||
                user.RefreshToken != refreshToken) return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _repositoryUser.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }

        public bool RevokeToken(string userName)
        {
            return _repositoryUser.RevokeToken(userName);
        }

        public User RegisterUser(UsuarioRegisterVO user)
        {
            User user1 = new User
            {
                FullName = user.NomeCompleto,
                Password = ComputerHash(user.Senha, new SHA256CryptoServiceProvider()),
                UserName = user.UserName,
                RefreshToken = _tokenService.GenerateRefreshToken(),
                RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry)
            };

            var usuarioRegistrado = _repository.Create(user1);

            if (usuarioRegistrado == null) return null;

            return usuarioRegistrado;
        }

        public string ComputerHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
}
}
