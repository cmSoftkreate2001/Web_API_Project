using Web_API_Project.Model;

namespace Web_API_Project.DAL.Interface
{
	public interface IFriendsService
	{
		FriendsModel CreateFriend(FriendsModel friend);
		FriendsModel GetFriendById(int friendId);
		FriendsModel UpdateFriend(FriendsModel friend);
		bool DeleteFriend(int friendId);
	}

}
