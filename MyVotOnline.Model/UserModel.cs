namespace MyVotOnline.Model
{
	public class UserModel
	{
		public int Id { get; set; }

		public string Fullname { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string Passwordhash { get; set; } = null!;

		public long Mobileno { get; set; }

		public int? Roleid { get; set; }

		public DateTime? Createdat { get; set; }

	}
}
