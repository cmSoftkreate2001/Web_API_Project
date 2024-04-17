using Microsoft.Data.SqlClient;
using Web_API_Project.DAL.Interface;
using Web_API_Project.Model;

namespace Web_API_Project.DAL.Service
{
	public class FriendsService : IFriendsService
	{
		private readonly string _connectionString;

		public FriendsService(string connectionString)
		{
			_connectionString = connectionString;
		}

		public FriendsModel CreateFriend(FriendsModel friend)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO Friends ( UserId,Name, ProfileImage) VALUES (@UserId,@Name, @ProfileImage);";
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Name", friend.Name);
					command.Parameters.AddWithValue("@ProfileImage", friend.ProfileImage);
					command.Parameters.AddWithValue("@UserId", friend.UserId);
					
					var newFriend = command.ExecuteScalar();
					var newFriendId = Convert.ToInt32(newFriend);
					friend.FriendId = newFriendId;
					return friend;
					
				}
			}
		}

		public FriendsModel GetFriendById(int friendId)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM Friends WHERE FriendId = @FriendId";
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@FriendId", friendId);
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return new FriendsModel
							{
								FriendId = (int)reader["FriendId"],
								Name = reader["Name"].ToString(),
								ProfileImage = reader["ProfileImage"].ToString()
							};
						}
						return null;
					}
				}
			}
		}

		public FriendsModel UpdateFriend(FriendsModel friend)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "UPDATE Friends SET Name = @Name, ProfileImage = @ProfileImage WHERE FriendId = @FriendId";
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Name", friend.Name);
					command.Parameters.AddWithValue("@ProfileImage", friend.ProfileImage);
					command.Parameters.AddWithValue("@FriendId", friend.FriendId);
					var rowsAffected = command.ExecuteNonQuery();
					if (rowsAffected > 0)
					{
						return friend;
					}
					return null; 
				}
			}
		}

		public bool DeleteFriend(int friendId)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "DELETE FROM Friends WHERE FriendId = @FriendId";
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@FriendId", friendId);
					var rowsAffected = command.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
	}


}
