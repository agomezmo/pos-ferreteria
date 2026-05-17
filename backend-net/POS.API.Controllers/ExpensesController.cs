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
public class ExpensesController : ControllerBase
{
	private readonly AppDbContext _context;

	public ExpensesController(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CGetExpenses_003Ed__2))]
	[HttpGet]
	public System.Threading.Tasks.Task<ActionResult<List<ExpenseDTO>>> GetExpenses([FromQuery] System.DateTime? startDate, [FromQuery] System.DateTime? endDate)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetExpenses_003Ed__2 _003CGetExpenses_003Ed__ = default(_003CGetExpenses_003Ed__2);
		_003CGetExpenses_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<ExpenseDTO>>>.Create();
		_003CGetExpenses_003Ed__._003C_003E4__this = this;
		_003CGetExpenses_003Ed__.startDate = startDate;
		_003CGetExpenses_003Ed__.endDate = endDate;
		_003CGetExpenses_003Ed__._003C_003E1__state = -1;
		_003CGetExpenses_003Ed__._003C_003Et__builder.Start<_003CGetExpenses_003Ed__2>(ref _003CGetExpenses_003Ed__);
		return _003CGetExpenses_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateExpense_003Ed__3))]
	[HttpPost]
	public System.Threading.Tasks.Task<ActionResult<ExpenseDTO>> CreateExpense([FromBody] CreateExpenseRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateExpense_003Ed__3 _003CCreateExpense_003Ed__ = default(_003CCreateExpense_003Ed__3);
		_003CCreateExpense_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<ExpenseDTO>>.Create();
		_003CCreateExpense_003Ed__._003C_003E4__this = this;
		_003CCreateExpense_003Ed__.request = request;
		_003CCreateExpense_003Ed__._003C_003E1__state = -1;
		_003CCreateExpense_003Ed__._003C_003Et__builder.Start<_003CCreateExpense_003Ed__3>(ref _003CCreateExpense_003Ed__);
		return _003CCreateExpense_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CDeleteExpense_003Ed__4))]
	[HttpDelete("{id}")]
	public System.Threading.Tasks.Task<ActionResult> DeleteExpense(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CDeleteExpense_003Ed__4 _003CDeleteExpense_003Ed__ = default(_003CDeleteExpense_003Ed__4);
		_003CDeleteExpense_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult>.Create();
		_003CDeleteExpense_003Ed__._003C_003E4__this = this;
		_003CDeleteExpense_003Ed__.id = id;
		_003CDeleteExpense_003Ed__._003C_003E1__state = -1;
		_003CDeleteExpense_003Ed__._003C_003Et__builder.Start<_003CDeleteExpense_003Ed__4>(ref _003CDeleteExpense_003Ed__);
		return _003CDeleteExpense_003Ed__._003C_003Et__builder.get_Task();
	}
}
