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
public class SuppliersController : ControllerBase
{
	private readonly AppDbContext _context;

	public SuppliersController(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CUpdateSupplier_003Ed__2))]
	[HttpPut("{id}")]
	public System.Threading.Tasks.Task<ActionResult<SupplierDTO>> UpdateSupplier(int id, [FromBody] CreateSupplierRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CUpdateSupplier_003Ed__2 _003CUpdateSupplier_003Ed__ = default(_003CUpdateSupplier_003Ed__2);
		_003CUpdateSupplier_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<SupplierDTO>>.Create();
		_003CUpdateSupplier_003Ed__._003C_003E4__this = this;
		_003CUpdateSupplier_003Ed__.id = id;
		_003CUpdateSupplier_003Ed__.request = request;
		_003CUpdateSupplier_003Ed__._003C_003E1__state = -1;
		_003CUpdateSupplier_003Ed__._003C_003Et__builder.Start<_003CUpdateSupplier_003Ed__2>(ref _003CUpdateSupplier_003Ed__);
		return _003CUpdateSupplier_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CDeleteSupplier_003Ed__3))]
	[HttpDelete("{id}")]
	public System.Threading.Tasks.Task<ActionResult> DeleteSupplier(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CDeleteSupplier_003Ed__3 _003CDeleteSupplier_003Ed__ = default(_003CDeleteSupplier_003Ed__3);
		_003CDeleteSupplier_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult>.Create();
		_003CDeleteSupplier_003Ed__._003C_003E4__this = this;
		_003CDeleteSupplier_003Ed__.id = id;
		_003CDeleteSupplier_003Ed__._003C_003E1__state = -1;
		_003CDeleteSupplier_003Ed__._003C_003Et__builder.Start<_003CDeleteSupplier_003Ed__3>(ref _003CDeleteSupplier_003Ed__);
		return _003CDeleteSupplier_003Ed__._003C_003Et__builder.get_Task();
	}
}
