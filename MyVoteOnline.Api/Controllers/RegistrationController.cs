using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVoteOnline.Services.Interfaces;
using MyVotOnline.Model;

namespace MyVoteOnline.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegistrationController(IUserRepository userRepository) : ControllerBase
	{
		private readonly IUserRepository _userRepository = userRepository;

		[HttpPost]
		public async Task<IActionResult> Post(UserModel user)
		{
			try
			{
				var result = await _userRepository.AddUser(user);
				return Ok(new { Status = "success", Result = result });
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });

			}
			}
		}
	}

