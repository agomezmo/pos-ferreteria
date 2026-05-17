using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.API.DTOs;
using POS.API.Data;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
	private readonly AppDbContext _context;

	public UsersController(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CUpdateUser_003Ed__2))]
	[HttpPut("{id}")]
	public System.Threading.Tasks.Task<ActionResult<UserDTO>> UpdateUser(int id, [FromBody] UpdateUserRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CUpdateUser_003Ed__2 _003CUpdateUser_003Ed__ = default(_003CUpdateUser_003Ed__2);
		_003CUpdateUser_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<UserDTO>>.Create();
		_003CUpdateUser_003Ed__._003C_003E4__this = this;
		_003CUpdateUser_003Ed__.id = id;
		_003CUpdateUser_003Ed__.request = request;
		_003CUpdateUser_003Ed__._003C_003E1__state = -1;
		_003CUpdateUser_003Ed__._003C_003Et__builder.Start<_003CUpdateUser_003Ed__2>(ref _003CUpdateUser_003Ed__);
		return _003CUpdateUser_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CDeleteUser_003Ed__3))]
	[HttpDelete("{id}")]
	public System.Threading.Tasks.Task<ActionResult> DeleteUser(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CDeleteUser_003Ed__3 _003CDeleteUser_003Ed__ = default(_003CDeleteUser_003Ed__3);
		_003CDeleteUser_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult>.Create();
		_003CDeleteUser_003Ed__._003C_003E4__this = this;
		_003CDeleteUser_003Ed__.id = id;
		_003CDeleteUser_003Ed__._003C_003E1__state = -1;
		_003CDeleteUser_003Ed__._003C_003Et__builder.Start<_003CDeleteUser_003Ed__3>(ref _003CDeleteUser_003Ed__);
		return _003CDeleteUser_003Ed__._003C_003Et__builder.get_Task();
	}
}
