using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using POS.API.DTOs;
using POS.API.Data;

namespace POS.API.Services;

public class SaleService
{
	private readonly AppDbContext _context;

	public SaleService(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CCreateSaleAsync_003Ed__2))]
	public System.Threading.Tasks.Task<SaleDTO> CreateSaleAsync(CreateSaleRequest request, int userId)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateSaleAsync_003Ed__2 _003CCreateSaleAsync_003Ed__ = default(_003CCreateSaleAsync_003Ed__2);
		_003CCreateSaleAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<SaleDTO>.Create();
		_003CCreateSaleAsync_003Ed__._003C_003E4__this = this;
		_003CCreateSaleAsync_003Ed__.request = request;
		_003CCreateSaleAsync_003Ed__.userId = userId;
		_003CCreateSaleAsync_003Ed__._003C_003E1__state = -1;
		_003CCreateSaleAsync_003Ed__._003C_003Et__builder.Start<_003CCreateSaleAsync_003Ed__2>(ref _003CCreateSaleAsync_003Ed__);
		return _003CCreateSaleAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetSaleByIdAsync_003Ed__3))]
	public System.Threading.Tasks.Task<SaleDTO?> GetSaleByIdAsync(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetSaleByIdAsync_003Ed__3 _003CGetSaleByIdAsync_003Ed__ = default(_003CGetSaleByIdAsync_003Ed__3);
		_003CGetSaleByIdAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<SaleDTO>.Create();
		_003CGetSaleByIdAsync_003Ed__._003C_003E4__this = this;
		_003CGetSaleByIdAsync_003Ed__.id = id;
		_003CGetSaleByIdAsync_003Ed__._003C_003E1__state = -1;
		_003CGetSaleByIdAsync_003Ed__._003C_003Et__builder.Start<_003CGetSaleByIdAsync_003Ed__3>(ref _003CGetSaleByIdAsync_003Ed__);
		return _003CGetSaleByIdAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetSalesAsync_003Ed__4))]
	public System.Threading.Tasks.Task<List<SaleListDTO>> GetSalesAsync(System.DateTime? startDate = null, System.DateTime? endDate = null)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetSalesAsync_003Ed__4 _003CGetSalesAsync_003Ed__ = default(_003CGetSalesAsync_003Ed__4);
		_003CGetSalesAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<List<SaleListDTO>>.Create();
		_003CGetSalesAsync_003Ed__._003C_003E4__this = this;
		_003CGetSalesAsync_003Ed__.startDate = startDate;
		_003CGetSalesAsync_003Ed__.endDate = endDate;
		_003CGetSalesAsync_003Ed__._003C_003E1__state = -1;
		_003CGetSalesAsync_003Ed__._003C_003Et__builder.Start<_003CGetSalesAsync_003Ed__4>(ref _003CGetSalesAsync_003Ed__);
		return _003CGetSalesAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGenerateReceiptNumberAsync_003Ed__5))]
	private System.Threading.Tasks.Task<string> GenerateReceiptNumberAsync()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGenerateReceiptNumberAsync_003Ed__5 _003CGenerateReceiptNumberAsync_003Ed__ = default(_003CGenerateReceiptNumberAsync_003Ed__5);
		_003CGenerateReceiptNumberAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<string>.Create();
		_003CGenerateReceiptNumberAsync_003Ed__._003C_003E4__this = this;
		_003CGenerateReceiptNumberAsync_003Ed__._003C_003E1__state = -1;
		_003CGenerateReceiptNumberAsync_003Ed__._003C_003Et__builder.Start<_003CGenerateReceiptNumberAsync_003Ed__5>(ref _003CGenerateReceiptNumberAsync_003Ed__);
		return _003CGenerateReceiptNumberAsync_003Ed__._003C_003Et__builder.get_Task();
	}
}
