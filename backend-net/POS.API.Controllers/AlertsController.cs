using System.Collections.Generic;
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
public class AlertsController : ControllerBase
{
	private readonly AppDbContext _context;

	public AlertsController(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CGetAlerts_003Ed__2))]
	[HttpGet]
	public System.Threading.Tasks.Task<ActionResult<List<AlertDTO>>> GetAlerts()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetAlerts_003Ed__2 _003CGetAlerts_003Ed__ = default(_003CGetAlerts_003Ed__2);
		_003CGetAlerts_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<AlertDTO>>>.Create();
		_003CGetAlerts_003Ed__._003C_003E4__this = this;
		_003CGetAlerts_003Ed__._003C_003E1__state = -1;
		_003CGetAlerts_003Ed__._003C_003Et__builder.Start<_003CGetAlerts_003Ed__2>(ref _003CGetAlerts_003Ed__);
		return _003CGetAlerts_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CMarkAsRead_003Ed__3))]
	[HttpPost("{id}/read")]
	public System.Threading.Tasks.Task<ActionResult> MarkAsRead(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CMarkAsRead_003Ed__3 _003CMarkAsRead_003Ed__ = default(_003CMarkAsRead_003Ed__3);
		_003CMarkAsRead_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult>.Create();
		_003CMarkAsRead_003Ed__._003C_003E4__this = this;
		_003CMarkAsRead_003Ed__.id = id;
		_003CMarkAsRead_003Ed__._003C_003E1__state = -1;
		_003CMarkAsRead_003Ed__._003C_003Et__builder.Start<_003CMarkAsRead_003Ed__3>(ref _003CMarkAsRead_003Ed__);
		return _003CMarkAsRead_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CMarkAllAsRead_003Ed__4))]
	[HttpPost("read-all")]
	public System.Threading.Tasks.Task<ActionResult> MarkAllAsRead()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CMarkAllAsRead_003Ed__4 _003CMarkAllAsRead_003Ed__ = default(_003CMarkAllAsRead_003Ed__4);
		_003CMarkAllAsRead_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult>.Create();
		_003CMarkAllAsRead_003Ed__._003C_003E4__this = this;
		_003CMarkAllAsRead_003Ed__._003C_003E1__state = -1;
		_003CMarkAllAsRead_003Ed__._003C_003Et__builder.Start<_003CMarkAllAsRead_003Ed__4>(ref _003CMarkAllAsRead_003Ed__);
		return _003CMarkAllAsRead_003Ed__._003C_003Et__builder.get_Task();
	}
}
