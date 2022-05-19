using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Repository
{
    public interface IUsersRepository
    {
        User ValidateCredentials(UserVO user);

        User ValidateCredentials(string user);

        bool RevokeToken(string username);

        User RefreshUserInfo(User user);
        
    
    }
}
