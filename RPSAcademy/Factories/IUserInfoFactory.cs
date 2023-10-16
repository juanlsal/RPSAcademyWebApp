using RPSAcademy.Models;

namespace RPSAcademy.Factories
{
    public interface IUserInfoFactory
    {
        public UserInfo CreateUserInfo(string userId, string preferredName);
    }
}
