using RPSAcademy.Models;


namespace RPSAcademy.Factories
{
    public class UserInfoFactory : IUserInfoFactory
    {
        /// <summary>
        /// Creates instances of object UserInfo
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="preferredName"></param>
        /// <returns>UserInfo linked to userId and a preferred name</returns>
        public UserInfo CreateUserInfo(string userId, string preferredName)
        {
            return new UserInfo
            {
                UserId = userId,
                PreferredName = preferredName,
            };
        }
    }
}
