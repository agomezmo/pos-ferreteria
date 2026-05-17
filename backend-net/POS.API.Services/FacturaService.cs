using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using POS.API.DTOs;
using POS.API.Data;

namespace POS.API.Services;

public class FacturaService
{
	private readonly AppDbContext _context;

	public FacturaService(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CGetFacturasAsync_003Ed__2))]
	public System.Threading.Tasks.Task<List<FacturaDTO>> GetFacturasAsync()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetFacturasAsync_003Ed__2 _003CGetFacturasAsync_003Ed__ = default(_003CGetFacturasAsync_003Ed__2);
		_003CGetFacturasAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<List<FacturaDTO>>.Create();
		_003CGetFacturasAsync_003Ed__._003C_003E4__this = this;
		_003CGetFacturasAsync_003Ed__._003C_003E1__state = -1;
		_003CGetFacturasAsync_003Ed__._003C_003Et__builder.Start<_003CGetFacturasAsync_003Ed__2>(ref _003CGetFacturasAsync_003Ed__);
		return _003CGetFacturasAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetFacturaByIdAsync_003Ed__3))]
	public System.Threading.Tasks.Task<FacturaDTO?> GetFacturaByIdAsync(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetFacturaByIdAsync_003Ed__3 _003CGetFacturaByIdAsync_003Ed__ = default(_003CGetFacturaByIdAsync_003Ed__3);
		_003CGetFacturaByIdAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<FacturaDTO>.Create();
		_003CGetFacturaByIdAsync_003Ed__._003C_003E4__this = this;
		_003CGetFacturaByIdAsync_003Ed__.id = id;
		_003CGetFacturaByIdAsync_003Ed__._003C_003E1__state = -1;
		_003CGetFacturaByIdAsync_003Ed__._003C_003Et__builder.Start<_003CGetFacturaByIdAsync_003Ed__3>(ref _003CGetFacturaByIdAsync_003Ed__);
		return _003CGetFacturaByIdAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateFacturaAsync_003Ed__4))]
	public System.Threading.Tasks.Task<FacturaDTO?> CreateFacturaAsync(FacturarRequest request, int userId)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateFacturaAsync_003Ed__4 _003CCreateFacturaAsync_003Ed__ = default(_003CCreateFacturaAsync_003Ed__4);
		_003CCreateFacturaAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<FacturaDTO>.Create();
		_003CCreateFacturaAsync_003Ed__._003C_003E4__this = this;
		_003CCreateFacturaAsync_003Ed__.request = request;
		_003CCreateFacturaAsync_003Ed__.userId = userId;
		_003CCreateFacturaAsync_003Ed__._003C_003E1__state = -1;
		_003CCreateFacturaAsync_003Ed__._003C_003Et__builder.Start<_003CCreateFacturaAsync_003Ed__4>(ref _003CCreateFacturaAsync_003Ed__);
		return _003CCreateFacturaAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCancelFacturaAsync_003Ed__5))]
	public System.Threading.Tasks.Task<bool> CancelFacturaAsync(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCancelFacturaAsync_003Ed__5 _003CCancelFacturaAsync_003Ed__ = default(_003CCancelFacturaAsync_003Ed__5);
		_003CCancelFacturaAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
		_003CCancelFacturaAsync_003Ed__._003C_003E4__this = this;
		_003CCancelFacturaAsync_003Ed__.id = id;
		_003CCancelFacturaAsync_003Ed__._003C_003E1__state = -1;
		_003CCancelFacturaAsync_003Ed__._003C_003Et__builder.Start<_003CCancelFacturaAsync_003Ed__5>(ref _003CCancelFacturaAsync_003Ed__);
		return _003CCancelFacturaAsync_003Ed__._003C_003Et__builder.get_Task();
	}
}
