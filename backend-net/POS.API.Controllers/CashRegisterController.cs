using System;
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
public class CashRegisterController : ControllerBase
{
	private readonly CashRegisterService _cashRegisterService;

	public CashRegisterController(CashRegisterService cashRegisterService)
	{
		_cashRegisterService = cashRegisterService;
	}

	[AsyncStateMachine(typeof(_003CGetCashRegisters_003Ed__2))]
	[HttpGet]
	public System.Threading.Tasks.Task<ActionResult<List<CashRegisterDTO>>> GetCashRegisters()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetCashRegisters_003Ed__2 _003CGetCashRegisters_003Ed__ = default(_003CGetCashRegisters_003Ed__2);
		_003CGetCashRegisters_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<CashRegisterDTO>>>.Create();
		_003CGetCashRegisters_003Ed__._003C_003E4__this = this;
		_003CGetCashRegisters_003Ed__._003C_003E1__state = -1;
		_003CGetCashRegisters_003Ed__._003C_003Et__builder.Start<_003CGetCashRegisters_003Ed__2>(ref _003CGetCashRegisters_003Ed__);
		return _003CGetCashRegisters_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateCashRegister_003Ed__3))]
	[HttpPost]
	public System.Threading.Tasks.Task<ActionResult<CashRegisterDTO>> CreateCashRegister([FromBody] CreateCashRegisterRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateCashRegister_003Ed__3 _003CCreateCashRegister_003Ed__ = default(_003CCreateCashRegister_003Ed__3);
		_003CCreateCashRegister_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<CashRegisterDTO>>.Create();
		_003CCreateCashRegister_003Ed__._003C_003E4__this = this;
		_003CCreateCashRegister_003Ed__.request = request;
		_003CCreateCashRegister_003Ed__._003C_003E1__state = -1;
		_003CCreateCashRegister_003Ed__._003C_003Et__builder.Start<_003CCreateCashRegister_003Ed__3>(ref _003CCreateCashRegister_003Ed__);
		return _003CCreateCashRegister_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003COpenSession_003Ed__4))]
	[HttpPost("sessions/open")]
	public System.Threading.Tasks.Task<ActionResult<CashRegisterSessionDTO>> OpenSession([FromBody] OpenSessionRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003COpenSession_003Ed__4 _003COpenSession_003Ed__ = default(_003COpenSession_003Ed__4);
		_003COpenSession_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<CashRegisterSessionDTO>>.Create();
		_003COpenSession_003Ed__._003C_003E4__this = this;
		_003COpenSession_003Ed__.request = request;
		_003COpenSession_003Ed__._003C_003E1__state = -1;
		_003COpenSession_003Ed__._003C_003Et__builder.Start<_003COpenSession_003Ed__4>(ref _003COpenSession_003Ed__);
		return _003COpenSession_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCloseSession_003Ed__5))]
	[HttpPost("sessions/{id}/close")]
	public System.Threading.Tasks.Task<ActionResult<CashRegisterSessionDTO>> CloseSession(int id, [FromBody] CloseSessionRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCloseSession_003Ed__5 _003CCloseSession_003Ed__ = default(_003CCloseSession_003Ed__5);
		_003CCloseSession_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<CashRegisterSessionDTO>>.Create();
		_003CCloseSession_003Ed__._003C_003E4__this = this;
		_003CCloseSession_003Ed__.id = id;
		_003CCloseSession_003Ed__.request = request;
		_003CCloseSession_003Ed__._003C_003E1__state = -1;
		_003CCloseSession_003Ed__._003C_003Et__builder.Start<_003CCloseSession_003Ed__5>(ref _003CCloseSession_003Ed__);
		return _003CCloseSession_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetCurrentSession_003Ed__6))]
	[HttpGet("sessions/current/{cashRegisterId}")]
	public System.Threading.Tasks.Task<ActionResult<CashRegisterSessionDTO>> GetCurrentSession(int cashRegisterId)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetCurrentSession_003Ed__6 _003CGetCurrentSession_003Ed__ = default(_003CGetCurrentSession_003Ed__6);
		_003CGetCurrentSession_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<CashRegisterSessionDTO>>.Create();
		_003CGetCurrentSession_003Ed__._003C_003E4__this = this;
		_003CGetCurrentSession_003Ed__.cashRegisterId = cashRegisterId;
		_003CGetCurrentSession_003Ed__._003C_003E1__state = -1;
		_003CGetCurrentSession_003Ed__._003C_003Et__builder.Start<_003CGetCurrentSession_003Ed__6>(ref _003CGetCurrentSession_003Ed__);
		return _003CGetCurrentSession_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetSessions_003Ed__7))]
	[HttpGet("sessions")]
	public System.Threading.Tasks.Task<ActionResult<List<CashRegisterSessionDTO>>> GetSessions([FromQuery] System.DateTime? startDate, [FromQuery] System.DateTime? endDate)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetSessions_003Ed__7 _003CGetSessions_003Ed__ = default(_003CGetSessions_003Ed__7);
		_003CGetSessions_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<CashRegisterSessionDTO>>>.Create();
		_003CGetSessions_003Ed__._003C_003E4__this = this;
		_003CGetSessions_003Ed__.startDate = startDate;
		_003CGetSessions_003Ed__.endDate = endDate;
		_003CGetSessions_003Ed__._003C_003E1__state = -1;
		_003CGetSessions_003Ed__._003C_003Et__builder.Start<_003CGetSessions_003Ed__7>(ref _003CGetSessions_003Ed__);
		return _003CGetSessions_003Ed__._003C_003Et__builder.get_Task();
	}
}
