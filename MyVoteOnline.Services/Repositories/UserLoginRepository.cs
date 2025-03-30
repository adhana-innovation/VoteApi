using Microsoft.EntityFrameworkCore;
using MyVoteOnline.Services.Interfaces;
using MyVoteOnline.Services.Utilty;
using MyVotOnline.DataBaseLayer.DataContext;
using MyVotOnline.Model;

namespace MyVoteOnline.Services.Repositories
{
	public class UserLoginRepository(VoteContext context) : ILoginRepository
	{
		private readonly VoteContext _context = context;

		public async Task<bool> LoginUser(LoginRequests requests)
		{
			bool verifypasswords = false;
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == requests.Email);
			if (user != null && user.Passwordhash != null)
			{
				verifypasswords = PasswordHelper.VerifyPassword(requests.Password, user.Passwordhash);
			}
			return verifypasswords;
		}
	}
}
