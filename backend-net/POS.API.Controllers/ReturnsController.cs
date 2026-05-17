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
public class ReturnsController : ControllerBase
{
	private readonly AppDbContext _context;

	public ReturnsController(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CGetReturns_003Ed__2))]
	[HttpGet]
	public System.Threading.Tasks.Task<ActionResult<List<ReturnDTO>>> GetReturns()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetReturns_003Ed__2 _003CGetReturns_003Ed__ = default(_003CGetReturns_003Ed__2);
		_003CGetReturns_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<ReturnDTO>>>.Create();
		_003CGetReturns_003Ed__._003C_003E4__this = this;
		_003CGetReturns_003Ed__._003C_003E1__state = -1;
		_003CGetReturns_003Ed__._003C_003Et__builder.Start<_003CGetReturns_003Ed__2>(ref _003CGetReturns_003Ed__);
		return _003CGetReturns_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateReturn_003Ed__3))]
	[HttpPost]
	public System.Threading.Tasks.Task<ActionResult<ReturnDTO>> CreateReturn([FromBody] CreateReturnRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateReturn_003Ed__3 _003CCreateReturn_003Ed__ = default(_003CCreateReturn_003Ed__3);
		_003CCreateReturn_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<ReturnDTO>>.Create();
		_003CCreateReturn_003Ed__._003C_003E4__this = this;
		_003CCreateReturn_003Ed__.request = request;
		_003CCreateReturn_003Ed__._003C_003E1__state = -1;
		_003CCreateReturn_003Ed__._003C_003Et__builder.Start<_003CCreateReturn_003Ed__3>(ref _003CCreateReturn_003Ed__);
		return _003CCreateReturn_003Ed__._003C_003Et__builder.get_Task();
	}
}
