using System;
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
public class InventoryController : ControllerBase
{
	private readonly AppDbContext _context;

	public InventoryController(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CGetMovements_003Ed__2))]
	[HttpGet("movements")]
	public System.Threading.Tasks.Task<ActionResult<List<InventoryMovementDTO>>> GetMovements([FromQuery] int? productId, [FromQuery] System.DateTime? startDate, [FromQuery] System.DateTime? endDate)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetMovements_003Ed__2 _003CGetMovements_003Ed__ = default(_003CGetMovements_003Ed__2);
		_003CGetMovements_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<InventoryMovementDTO>>>.Create();
		_003CGetMovements_003Ed__._003C_003E4__this = this;
		_003CGetMovements_003Ed__.productId = productId;
		_003CGetMovements_003Ed__.startDate = startDate;
		_003CGetMovements_003Ed__.endDate = endDate;
		_003CGetMovements_003Ed__._003C_003E1__state = -1;
		_003CGetMovements_003Ed__._003C_003Et__builder.Start<_003CGetMovements_003Ed__2>(ref _003CGetMovements_003Ed__);
		return _003CGetMovements_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateMovement_003Ed__3))]
	[HttpPost("movements")]
	public System.Threading.Tasks.Task<ActionResult<InventoryMovementDTO>> CreateMovement([FromBody] CreateInventoryMovementRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateMovement_003Ed__3 _003CCreateMovement_003Ed__ = default(_003CCreateMovement_003Ed__3);
		_003CCreateMovement_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<InventoryMovementDTO>>.Create();
		_003CCreateMovement_003Ed__._003C_003E4__this = this;
		_003CCreateMovement_003Ed__.request = request;
		_003CCreateMovement_003Ed__._003C_003E1__state = -1;
		_003CCreateMovement_003Ed__._003C_003Et__builder.Start<_003CCreateMovement_003Ed__3>(ref _003CCreateMovement_003Ed__);
		return _003CCreateMovement_003Ed__._003C_003Et__builder.get_Task();
	}
}
