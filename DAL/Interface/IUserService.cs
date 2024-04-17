using Web_API_Project.Model;

namespace Web_API_Project.DAL.Interface
{
	public interface IUserService
	{
		UserModel CreateUser(UserModel user);
		UserModel GetUserById(int userId);
		UserModel UpdateUser(UserModel user);
		bool DeleteUser(int userId);
	}
}
