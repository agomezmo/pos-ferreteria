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
public class CustomersController : ControllerBase
{
	private readonly AppDbContext _context;

	public CustomersController(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CGetCustomers_003Ed__2))]
	[HttpGet]
	public System.Threading.Tasks.Task<ActionResult<List<CustomerDTO>>> GetCustomers()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetCustomers_003Ed__2 _003CGetCustomers_003Ed__ = default(_003CGetCustomers_003Ed__2);
		_003CGetCustomers_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<CustomerDTO>>>.Create();
		_003CGetCustomers_003Ed__._003C_003E4__this = this;
		_003CGetCustomers_003Ed__._003C_003E1__state = -1;
		_003CGetCustomers_003Ed__._003C_003Et__builder.Start<_003CGetCustomers_003Ed__2>(ref _003CGetCustomers_003Ed__);
		return _003CGetCustomers_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetCustomer_003Ed__3))]
	[HttpGet("{id}")]
	public System.Threading.Tasks.Task<ActionResult<CustomerDTO>> GetCustomer(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetCustomer_003Ed__3 _003CGetCustomer_003Ed__ = default(_003CGetCustomer_003Ed__3);
		_003CGetCustomer_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<CustomerDTO>>.Create();
		_003CGetCustomer_003Ed__._003C_003E4__this = this;
		_003CGetCustomer_003Ed__.id = id;
		_003CGetCustomer_003Ed__._003C_003E1__state = -1;
		_003CGetCustomer_003Ed__._003C_003Et__builder.Start<_003CGetCustomer_003Ed__3>(ref _003CGetCustomer_003Ed__);
		return _003CGetCustomer_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CSearchCustomers_003Ed__4))]
	[HttpGet("search")]
	public System.Threading.Tasks.Task<ActionResult<List<CustomerDTO>>> SearchCustomers([FromQuery] string q)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CSearchCustomers_003Ed__4 _003CSearchCustomers_003Ed__ = default(_003CSearchCustomers_003Ed__4);
		_003CSearchCustomers_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<CustomerDTO>>>.Create();
		_003CSearchCustomers_003Ed__._003C_003E4__this = this;
		_003CSearchCustomers_003Ed__.q = q;
		_003CSearchCustomers_003Ed__._003C_003E1__state = -1;
		_003CSearchCustomers_003Ed__._003C_003Et__builder.Start<_003CSearchCustomers_003Ed__4>(ref _003CSearchCustomers_003Ed__);
		return _003CSearchCustomers_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateCustomer_003Ed__5))]
	[HttpPost]
	public System.Threading.Tasks.Task<ActionResult<CustomerDTO>> CreateCustomer([FromBody] CreateCustomerRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateCustomer_003Ed__5 _003CCreateCustomer_003Ed__ = default(_003CCreateCustomer_003Ed__5);
		_003CCreateCustomer_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<CustomerDTO>>.Create();
		_003CCreateCustomer_003Ed__._003C_003E4__this = this;
		_003CCreateCustomer_003Ed__.request = request;
		_003CCreateCustomer_003Ed__._003C_003E1__state = -1;
		_003CCreateCustomer_003Ed__._003C_003Et__builder.Start<_003CCreateCustomer_003Ed__5>(ref _003CCreateCustomer_003Ed__);
		return _003CCreateCustomer_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CUpdateCustomer_003Ed__6))]
	[HttpPut("{id}")]
	public System.Threading.Tasks.Task<ActionResult<CustomerDTO>> UpdateCustomer(int id, [FromBody] UpdateCustomerRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CUpdateCustomer_003Ed__6 _003CUpdateCustomer_003Ed__ = default(_003CUpdateCustomer_003Ed__6);
		_003CUpdateCustomer_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<CustomerDTO>>.Create();
		_003CUpdateCustomer_003Ed__._003C_003E4__this = this;
		_003CUpdateCustomer_003Ed__.id = id;
		_003CUpdateCustomer_003Ed__.request = request;
		_003CUpdateCustomer_003Ed__._003C_003E1__state = -1;
		_003CUpdateCustomer_003Ed__._003C_003Et__builder.Start<_003CUpdateCustomer_003Ed__6>(ref _003CUpdateCustomer_003Ed__);
		return _003CUpdateCustomer_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CDeleteCustomer_003Ed__7))]
	[HttpDelete("{id}")]
	public System.Threading.Tasks.Task<ActionResult> DeleteCustomer(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CDeleteCustomer_003Ed__7 _003CDeleteCustomer_003Ed__ = default(_003CDeleteCustomer_003Ed__7);
		_003CDeleteCustomer_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult>.Create();
		_003CDeleteCustomer_003Ed__._003C_003E4__this = this;
		_003CDeleteCustomer_003Ed__.id = id;
		_003CDeleteCustomer_003Ed__._003C_003E1__state = -1;
		_003CDeleteCustomer_003Ed__._003C_003Et__builder.Start<_003CDeleteCustomer_003Ed__7>(ref _003CDeleteCustomer_003Ed__);
		return _003CDeleteCustomer_003Ed__._003C_003Et__builder.get_Task();
	}
}
