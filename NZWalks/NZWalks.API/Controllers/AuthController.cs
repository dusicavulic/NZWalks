using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{

	[ApiController]
	[Route("[controller]")]
	public class AuthController : Controller
	{
		private readonly IUserRepository userRepository;
		private readonly ITokenHandler tokenHandler;

		public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
		{
			this.userRepository = userRepository;
			this.tokenHandler = tokenHandler;
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
		{
			var user = await userRepository.AuthenticateAsync(
				loginRequest.Username, loginRequest.Password);

			if (user != null)
			{
				var token = await tokenHandler.CreateTokenAsync(user);
				return Ok(token);
			
			}

			return BadRequest("Username or Password is incorrect.");
		}
	}
}

//{
//	"username": "readonly@user.com",
//  "password": "Readonly@user"
//}

//Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9naXZlbm5hbWUiOiJSZWFkIE9ubHkiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zdXJuYW1lIjoiVXNlciIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InJlYWRvbmx5QHVzZXIuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoicmVhZGVyIiwiZXhwIjoxNjc3NzkyOTMzLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MTM1LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcxMzUvIn0.hSYOzRtzKwhvl8R0XyhXqurzfE7VbJVAMBsiQ4Zw1Ps

//{
//"username": "readwrite@user.com",
//  "password": "Readwrite@user"
//}

//Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9naXZlbm5hbWUiOiJSZWFkIFdyaXRlIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc3VybmFtZSI6IlVzZXIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJyZWFkd3JpdGVAdXNlci5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsid3JpdGVyIiwicmVhZGVyIl0sImV4cCI6MTY3Nzg0MTMzNiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzEzNS8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MTM1LyJ9.xfaQe6jIBtRmmMnQP9EOHNplhZp29kiF-TXBFM4iiEs
