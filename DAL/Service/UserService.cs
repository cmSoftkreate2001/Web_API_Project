using Microsoft.Data.SqlClient;
using Web_API_Project.DAL.Interface;
using Web_API_Project.Model;

namespace Web_API_Project.DAL.Service
{
	public class UserService : IUserService
	{
		private readonly string _connectionString;

		public UserService(string connectionString)
		{
			_connectionString = connectionString;
		}

		public UserModel CreateUser(UserModel user)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO Users (Name, ProfileImage) VALUES (@Name, @ProfileImage);";
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Name", user.Name);
					command.Parameters.AddWithValue("@ProfileImage", user.ProfileImage);
					var newUser = command.ExecuteScalar();
					var newUserId = Convert.ToInt32(newUser);
					user.UserId = newUserId;
					return user;
					
				}
			}
		}


		public UserModel GetUserById(int userId)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM Users WHERE UserId = @UserId";
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@UserId", userId);
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return new UserModel
							{
								UserId = (int)reader["UserId"],
								Name = reader["Name"].ToString(),
								ProfileImage = reader["ProfileImage"].ToString(),
								
							};
						}
						return null; 
					}
				}
			}
		}

		public UserModel UpdateUser(UserModel user)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "UPDATE Users SET Name = @Name, ProfileImage = @ProfileImage WHERE UserId = @UserId";
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Name", user.Name);
					command.Parameters.AddWithValue("@ProfileImage", user.ProfileImage);
					command.Parameters.AddWithValue("@UserId", user.UserId);
					var rowsAffected = command.ExecuteNonQuery();
					if (rowsAffected > 0)
					{
						return user;
					}
					return null; 
				}
			}
		}

		public bool DeleteUser(int userId)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "DELETE FROM Users WHERE UserId = @UserId";
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@UserId", userId);
					var rowsAffected = command.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}

		

	}
}
