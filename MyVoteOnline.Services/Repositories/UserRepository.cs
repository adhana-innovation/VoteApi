using Microsoft.EntityFrameworkCore;
using MyVoteOnline.Services.Interfaces;
using MyVoteOnline.Services.Utilty;
using MyVotOnline.DataBaseLayer.DataContext;
using MyVotOnline.Model;

namespace MyVoteOnline.Services.Repositories
{
	public class UserRepository(VoteContext context) : IUserRepository
	{
		private readonly VoteContext _context = context;

		public async Task<int> AddUser(UserModel user)
		{
			int userId = 0;
			using (var transaction = _context.Database.BeginTransaction())
			{

				try
				{
					var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
					if (existingUser != null)
					{
						throw new Exception("Email is already Registered");
					}
					if (string.IsNullOrEmpty(user.Passwordhash))
					{
						throw new ArgumentNullException("Password cannot be Empty");
					}
					User newUser = new User
					{
						Fullname = user.Fullname,
						Email = user.Email,
						Passwordhash = PasswordHelper.HashPassword(user.Passwordhash),
						Roleid = user.Roleid,
						Mobileno = user.Mobileno
					};
					_context.Users.Add(newUser);
					_context.Entry(newUser).State = EntityState.Added;
					await _context.SaveChangesAsync();
					userId = newUser.Id;
					await transaction.CommitAsync();
				}
				catch
				{
					await transaction.RollbackAsync(); // Non-blocking
					throw;
				}
			}
			return userId;
		}
	}
}
