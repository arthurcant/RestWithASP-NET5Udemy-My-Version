using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO userCredentials);

        TokenVO ValidateCredentials(TokenVO token);
        User RegisterUser(UsuarioRegisterVO user);

        bool RevokeToken(string userName);
    }
}
