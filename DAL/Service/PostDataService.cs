using Microsoft.Data.SqlClient;
using Web_API_Project.DAL.Interface;
using Web_API_Project.Model;

namespace Web_API_Project.DAL.Service
{
	
		public class PostDataService : IPostDataService
		{
			private readonly string _connectionString;

			public PostDataService(string connectionString)
			{
				_connectionString = connectionString;
			}

			public PostDataModel CreatePostData(PostDataModel postData)
			{
				using (var connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					var sql = "INSERT INTO PostData (Name, ProfileImage, PostImage, Time) VALUES (@Name, @ProfileImage, @PostImage, @Time);";
					using (var command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@Name", postData.Name);
						command.Parameters.AddWithValue("@ProfileImage", postData.ProfileImage);
						command.Parameters.AddWithValue("@PostImage", postData.PostImage);
						command.Parameters.AddWithValue("@Time", DateTime.Now); 
						command.ExecuteNonQuery();
						return postData;
					}
				}
			}

			public PostDataModel GetPostDataById(int postId)
			{
				using (var connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					var sql = "SELECT Name, ProfileImage, PostImage, Time FROM PostData WHERE PostId = @PostId";
					using (var command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@PostId", postId);
						using (var reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								return new PostDataModel
								{
									Name = reader["Name"].ToString(),
									ProfileImage = reader["ProfileImage"].ToString(),
									PostImage = reader["PostImage"].ToString(),
									Time = reader["Time"].ToString() 
								};
							}
							return null;
						}
					}
				}
			}

			public PostDataModel UpdatePostData(PostDataModel postData)
			{
				using (var connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					var sql = "UPDATE PostData SET Name = @Name, ProfileImage = @ProfileImage, PostImage = @PostImage WHERE PostId = @PostId";
					using (var command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@Name", postData.Name);
						command.Parameters.AddWithValue("@ProfileImage", postData.ProfileImage);
						command.Parameters.AddWithValue("@PostImage", postData.PostImage);
					    command.Parameters.AddWithValue("@PostId", postData.PostId);



					var rowsAffected = command.ExecuteNonQuery();
						if (rowsAffected > 0)
						{
							
							return postData;
						}
						return null; 
					}
				}
			}

			public bool DeletePostData(int postId)
			{
				using (var connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					var sql = "DELETE FROM PostData WHERE PostId = @PostId";
					using (var command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@PostId", postId);
						var rowsAffected = command.ExecuteNonQuery();
						return rowsAffected > 0;
					}
				}
			}




		}
	
}
