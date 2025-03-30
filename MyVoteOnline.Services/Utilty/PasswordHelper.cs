namespace MyVoteOnline.Services.Utilty
{
	class PasswordHelper
	{
		//this is for convert password into hashpassword
		public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
		
		//this is for macth password at the time of login 
		public static bool VerifyPassword(string password,string hashedpassword)
		{
			return BCrypt.Net.BCrypt.Verify(password, hashedpassword);
		}
	}
}
