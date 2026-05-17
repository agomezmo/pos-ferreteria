using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.API.DTOs;
using POS.API.Services;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly AuthService _authService;

	public AuthController(AuthService authService)
	{
		_authService = authService;
	}

	[AsyncStateMachine(typeof(_003CLogin_003Ed__2))]
	[HttpPost("login")]
	public System.Threading.Tasks.Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CLogin_003Ed__2 _003CLogin_003Ed__ = default(_003CLogin_003Ed__2);
		_003CLogin_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<LoginResponse>>.Create();
		_003CLogin_003Ed__._003C_003E4__this = this;
		_003CLogin_003Ed__.request = request;
		_003CLogin_003Ed__._003C_003E1__state = -1;
		_003CLogin_003Ed__._003C_003Et__builder.Start<_003CLogin_003Ed__2>(ref _003CLogin_003Ed__);
		return _003CLogin_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetUsers_003Ed__3))]
	[Authorize]
	[HttpGet("users")]
	public System.Threading.Tasks.Task<ActionResult<List<UserDTO>>> GetUsers()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetUsers_003Ed__3 _003CGetUsers_003Ed__ = default(_003CGetUsers_003Ed__3);
		_003CGetUsers_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<UserDTO>>>.Create();
		_003CGetUsers_003Ed__._003C_003E4__this = this;
		_003CGetUsers_003Ed__._003C_003E1__state = -1;
		_003CGetUsers_003Ed__._003C_003Et__builder.Start<_003CGetUsers_003Ed__3>(ref _003CGetUsers_003Ed__);
		return _003CGetUsers_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateUser_003Ed__4))]
	[Authorize]
	[HttpPost("users")]
	public System.Threading.Tasks.Task<ActionResult<UserDTO>> CreateUser([FromBody] CreateUserRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateUser_003Ed__4 _003CCreateUser_003Ed__ = default(_003CCreateUser_003Ed__4);
		_003CCreateUser_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<UserDTO>>.Create();
		_003CCreateUser_003Ed__._003C_003E4__this = this;
		_003CCreateUser_003Ed__.request = request;
		_003CCreateUser_003Ed__._003C_003E1__state = -1;
		_003CCreateUser_003Ed__._003C_003Et__builder.Start<_003CCreateUser_003Ed__4>(ref _003CCreateUser_003Ed__);
		return _003CCreateUser_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CChangePassword_003Ed__5))]
	[Authorize]
	[HttpPost("change-password")]
	public System.Threading.Tasks.Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CChangePassword_003Ed__5 _003CChangePassword_003Ed__ = default(_003CChangePassword_003Ed__5);
		_003CChangePassword_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult>.Create();
		_003CChangePassword_003Ed__._003C_003E4__this = this;
		_003CChangePassword_003Ed__.request = request;
		_003CChangePassword_003Ed__._003C_003E1__state = -1;
		_003CChangePassword_003Ed__._003C_003Et__builder.Start<_003CChangePassword_003Ed__5>(ref _003CChangePassword_003Ed__);
		return _003CChangePassword_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetRoles_003Ed__6))]
	[Authorize]
	[HttpGet("roles")]
	public System.Threading.Tasks.Task<ActionResult<List<RoleDTO>>> GetRoles()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetRoles_003Ed__6 _003CGetRoles_003Ed__ = default(_003CGetRoles_003Ed__6);
		_003CGetRoles_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<RoleDTO>>>.Create();
		_003CGetRoles_003Ed__._003C_003E4__this = this;
		_003CGetRoles_003Ed__._003C_003E1__state = -1;
		_003CGetRoles_003Ed__._003C_003Et__builder.Start<_003CGetRoles_003Ed__6>(ref _003CGetRoles_003Ed__);
		return _003CGetRoles_003Ed__._003C_003Et__builder.get_Task();
	}
}
