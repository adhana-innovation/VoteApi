using MyVotOnline.Model;

namespace MyVoteOnline.Services.Interfaces
{
	public interface ILoginRepository
	{
		Task<bool> LoginUser(LoginRequests requests);
	}
}
