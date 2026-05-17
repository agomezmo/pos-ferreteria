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
[Authorize]
public class FacturasController : ControllerBase
{
	private readonly FacturaService _facturaService;

	public FacturasController(FacturaService facturaService)
	{
		_facturaService = facturaService;
	}

	[AsyncStateMachine(typeof(_003CGetFacturas_003Ed__2))]
	[HttpGet]
	public System.Threading.Tasks.Task<ActionResult<List<FacturaDTO>>> GetFacturas()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetFacturas_003Ed__2 _003CGetFacturas_003Ed__ = default(_003CGetFacturas_003Ed__2);
		_003CGetFacturas_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<FacturaDTO>>>.Create();
		_003CGetFacturas_003Ed__._003C_003E4__this = this;
		_003CGetFacturas_003Ed__._003C_003E1__state = -1;
		_003CGetFacturas_003Ed__._003C_003Et__builder.Start<_003CGetFacturas_003Ed__2>(ref _003CGetFacturas_003Ed__);
		return _003CGetFacturas_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetFactura_003Ed__3))]
	[HttpGet("{id}")]
	public System.Threading.Tasks.Task<ActionResult<FacturaDTO>> GetFactura(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetFactura_003Ed__3 _003CGetFactura_003Ed__ = default(_003CGetFactura_003Ed__3);
		_003CGetFactura_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<FacturaDTO>>.Create();
		_003CGetFactura_003Ed__._003C_003E4__this = this;
		_003CGetFactura_003Ed__.id = id;
		_003CGetFactura_003Ed__._003C_003E1__state = -1;
		_003CGetFactura_003Ed__._003C_003Et__builder.Start<_003CGetFactura_003Ed__3>(ref _003CGetFactura_003Ed__);
		return _003CGetFactura_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateFactura_003Ed__4))]
	[HttpPost]
	public System.Threading.Tasks.Task<ActionResult<FacturaDTO>> CreateFactura([FromBody] FacturarRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateFactura_003Ed__4 _003CCreateFactura_003Ed__ = default(_003CCreateFactura_003Ed__4);
		_003CCreateFactura_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<FacturaDTO>>.Create();
		_003CCreateFactura_003Ed__._003C_003E4__this = this;
		_003CCreateFactura_003Ed__.request = request;
		_003CCreateFactura_003Ed__._003C_003E1__state = -1;
		_003CCreateFactura_003Ed__._003C_003Et__builder.Start<_003CCreateFactura_003Ed__4>(ref _003CCreateFactura_003Ed__);
		return _003CCreateFactura_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCancelFactura_003Ed__5))]
	[HttpPost("{id}/cancel")]
	public System.Threading.Tasks.Task<ActionResult> CancelFactura(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCancelFactura_003Ed__5 _003CCancelFactura_003Ed__ = default(_003CCancelFactura_003Ed__5);
		_003CCancelFactura_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult>.Create();
		_003CCancelFactura_003Ed__._003C_003E4__this = this;
		_003CCancelFactura_003Ed__.id = id;
		_003CCancelFactura_003Ed__._003C_003E1__state = -1;
		_003CCancelFactura_003Ed__._003C_003Et__builder.Start<_003CCancelFactura_003Ed__5>(ref _003CCancelFactura_003Ed__);
		return _003CCancelFactura_003Ed__._003C_003Et__builder.get_Task();
	}
}
