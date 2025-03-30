using MyVotOnline.Model;

namespace MyVoteOnline.Services.Interfaces
{
	public interface IUserRepository
	{
	 public	Task<int> AddUser(UserModel user);
	}
}
