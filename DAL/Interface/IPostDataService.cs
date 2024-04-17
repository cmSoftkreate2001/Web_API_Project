using Web_API_Project.Model;

namespace Web_API_Project.DAL.Interface
{
	public interface IPostDataService
	{
		PostDataModel CreatePostData(PostDataModel postData);
		PostDataModel GetPostDataById(int postId);
		PostDataModel UpdatePostData(PostDataModel postData);
		bool DeletePostData(int postId);
	}
}
