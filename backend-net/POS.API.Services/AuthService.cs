using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using POS.API.DTOs;
using POS.API.Data;
using POS.API.Models;

namespace POS.API.Services;

public class AuthService
{
	private readonly AppDbContext _context;

	private readonly IConfiguration _configuration;

	public AuthService(AppDbContext context, IConfiguration configuration)
	{
		_context = context;
		_configuration = configuration;
	}

	[AsyncStateMachine(typeof(_003CLoginAsync_003Ed__3))]
	public System.Threading.Tasks.Task<LoginResponse?> LoginAsync(LoginRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CLoginAsync_003Ed__3 _003CLoginAsync_003Ed__ = default(_003CLoginAsync_003Ed__3);
		_003CLoginAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<LoginResponse>.Create();
		_003CLoginAsync_003Ed__._003C_003E4__this = this;
		_003CLoginAsync_003Ed__.request = request;
		_003CLoginAsync_003Ed__._003C_003E1__state = -1;
		_003CLoginAsync_003Ed__._003C_003Et__builder.Start<_003CLoginAsync_003Ed__3>(ref _003CLoginAsync_003Ed__);
		return _003CLoginAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetUsersAsync_003Ed__4))]
	public System.Threading.Tasks.Task<List<UserDTO>> GetUsersAsync()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetUsersAsync_003Ed__4 _003CGetUsersAsync_003Ed__ = default(_003CGetUsersAsync_003Ed__4);
		_003CGetUsersAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<List<UserDTO>>.Create();
		_003CGetUsersAsync_003Ed__._003C_003E4__this = this;
		_003CGetUsersAsync_003Ed__._003C_003E1__state = -1;
		_003CGetUsersAsync_003Ed__._003C_003Et__builder.Start<_003CGetUsersAsync_003Ed__4>(ref _003CGetUsersAsync_003Ed__);
		return _003CGetUsersAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateUserAsync_003Ed__5))]
	public System.Threading.Tasks.Task<UserDTO?> CreateUserAsync(CreateUserRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateUserAsync_003Ed__5 _003CCreateUserAsync_003Ed__ = default(_003CCreateUserAsync_003Ed__5);
		_003CCreateUserAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<UserDTO>.Create();
		_003CCreateUserAsync_003Ed__._003C_003E4__this = this;
		_003CCreateUserAsync_003Ed__.request = request;
		_003CCreateUserAsync_003Ed__._003C_003E1__state = -1;
		_003CCreateUserAsync_003Ed__._003C_003Et__builder.Start<_003CCreateUserAsync_003Ed__5>(ref _003CCreateUserAsync_003Ed__);
		return _003CCreateUserAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CChangePasswordAsync_003Ed__6))]
	public System.Threading.Tasks.Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CChangePasswordAsync_003Ed__6 _003CChangePasswordAsync_003Ed__ = default(_003CChangePasswordAsync_003Ed__6);
		_003CChangePasswordAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
		_003CChangePasswordAsync_003Ed__._003C_003E4__this = this;
		_003CChangePasswordAsync_003Ed__.userId = userId;
		_003CChangePasswordAsync_003Ed__.request = request;
		_003CChangePasswordAsync_003Ed__._003C_003E1__state = -1;
		_003CChangePasswordAsync_003Ed__._003C_003Et__builder.Start<_003CChangePasswordAsync_003Ed__6>(ref _003CChangePasswordAsync_003Ed__);
		return _003CChangePasswordAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetRolesAsync_003Ed__7))]
	public System.Threading.Tasks.Task<List<RoleDTO>> GetRolesAsync()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetRolesAsync_003Ed__7 _003CGetRolesAsync_003Ed__ = default(_003CGetRolesAsync_003Ed__7);
		_003CGetRolesAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<List<RoleDTO>>.Create();
		_003CGetRolesAsync_003Ed__._003C_003E4__this = this;
		_003CGetRolesAsync_003Ed__._003C_003E1__state = -1;
		_003CGetRolesAsync_003Ed__._003C_003Et__builder.Start<_003CGetRolesAsync_003Ed__7>(ref _003CGetRolesAsync_003Ed__);
		return _003CGetRolesAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	private string GenerateJwtToken(User user)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.get_UTF8().GetBytes(_configuration.get_Item("Jwt:Key")));
		SigningCredentials signingCredentials = new SigningCredentials(key, "HS256");
		System.DateTime dateTime = System.DateTime.get_UtcNow().AddMinutes(double.Parse(_configuration.get_Item("Jwt:ExpireMinutes") ?? "480"));
		Claim[] claims = (Claim[])(object)new Claim[4]
		{
			new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", user.Id.ToString()),
			new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", user.Username),
			new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", user.FullName),
			new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", user.Role.Name)
		};
		string issuer = _configuration.get_Item("Jwt:Issuer");
		string audience = _configuration.get_Item("Jwt:Audience");
		System.DateTime? expires = dateTime;
		SigningCredentials signingCredentials2 = signingCredentials;
		JwtSecurityToken token = new JwtSecurityToken(issuer, audience, claims, null, expires, signingCredentials2);
		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
