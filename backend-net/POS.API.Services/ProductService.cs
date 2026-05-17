using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using POS.API.DTOs;
using POS.API.Data;

namespace POS.API.Services;

public class ProductService
{
	private readonly AppDbContext _context;

	public ProductService(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CGetProductsAsync_003Ed__2))]
	public System.Threading.Tasks.Task<List<ProductDTO>> GetProductsAsync(bool includeInactive = false)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetProductsAsync_003Ed__2 _003CGetProductsAsync_003Ed__ = default(_003CGetProductsAsync_003Ed__2);
		_003CGetProductsAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<List<ProductDTO>>.Create();
		_003CGetProductsAsync_003Ed__._003C_003E4__this = this;
		_003CGetProductsAsync_003Ed__.includeInactive = includeInactive;
		_003CGetProductsAsync_003Ed__._003C_003E1__state = -1;
		_003CGetProductsAsync_003Ed__._003C_003Et__builder.Start<_003CGetProductsAsync_003Ed__2>(ref _003CGetProductsAsync_003Ed__);
		return _003CGetProductsAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetProductByIdAsync_003Ed__3))]
	public System.Threading.Tasks.Task<ProductDTO?> GetProductByIdAsync(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetProductByIdAsync_003Ed__3 _003CGetProductByIdAsync_003Ed__ = default(_003CGetProductByIdAsync_003Ed__3);
		_003CGetProductByIdAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ProductDTO>.Create();
		_003CGetProductByIdAsync_003Ed__._003C_003E4__this = this;
		_003CGetProductByIdAsync_003Ed__.id = id;
		_003CGetProductByIdAsync_003Ed__._003C_003E1__state = -1;
		_003CGetProductByIdAsync_003Ed__._003C_003Et__builder.Start<_003CGetProductByIdAsync_003Ed__3>(ref _003CGetProductByIdAsync_003Ed__);
		return _003CGetProductByIdAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CSearchProductsAsync_003Ed__4))]
	public System.Threading.Tasks.Task<List<ProductDTO>> SearchProductsAsync(string search)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CSearchProductsAsync_003Ed__4 _003CSearchProductsAsync_003Ed__ = default(_003CSearchProductsAsync_003Ed__4);
		_003CSearchProductsAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<List<ProductDTO>>.Create();
		_003CSearchProductsAsync_003Ed__._003C_003E4__this = this;
		_003CSearchProductsAsync_003Ed__.search = search;
		_003CSearchProductsAsync_003Ed__._003C_003E1__state = -1;
		_003CSearchProductsAsync_003Ed__._003C_003Et__builder.Start<_003CSearchProductsAsync_003Ed__4>(ref _003CSearchProductsAsync_003Ed__);
		return _003CSearchProductsAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateProductAsync_003Ed__5))]
	public System.Threading.Tasks.Task<ProductDTO> CreateProductAsync(CreateProductRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateProductAsync_003Ed__5 _003CCreateProductAsync_003Ed__ = default(_003CCreateProductAsync_003Ed__5);
		_003CCreateProductAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ProductDTO>.Create();
		_003CCreateProductAsync_003Ed__._003C_003E4__this = this;
		_003CCreateProductAsync_003Ed__.request = request;
		_003CCreateProductAsync_003Ed__._003C_003E1__state = -1;
		_003CCreateProductAsync_003Ed__._003C_003Et__builder.Start<_003CCreateProductAsync_003Ed__5>(ref _003CCreateProductAsync_003Ed__);
		return _003CCreateProductAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CUpdateProductAsync_003Ed__6))]
	public System.Threading.Tasks.Task<ProductDTO?> UpdateProductAsync(int id, UpdateProductRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CUpdateProductAsync_003Ed__6 _003CUpdateProductAsync_003Ed__ = default(_003CUpdateProductAsync_003Ed__6);
		_003CUpdateProductAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ProductDTO>.Create();
		_003CUpdateProductAsync_003Ed__._003C_003E4__this = this;
		_003CUpdateProductAsync_003Ed__.id = id;
		_003CUpdateProductAsync_003Ed__.request = request;
		_003CUpdateProductAsync_003Ed__._003C_003E1__state = -1;
		_003CUpdateProductAsync_003Ed__._003C_003Et__builder.Start<_003CUpdateProductAsync_003Ed__6>(ref _003CUpdateProductAsync_003Ed__);
		return _003CUpdateProductAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CDeleteProductAsync_003Ed__7))]
	public System.Threading.Tasks.Task<bool> DeleteProductAsync(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CDeleteProductAsync_003Ed__7 _003CDeleteProductAsync_003Ed__ = default(_003CDeleteProductAsync_003Ed__7);
		_003CDeleteProductAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
		_003CDeleteProductAsync_003Ed__._003C_003E4__this = this;
		_003CDeleteProductAsync_003Ed__.id = id;
		_003CDeleteProductAsync_003Ed__._003C_003E1__state = -1;
		_003CDeleteProductAsync_003Ed__._003C_003Et__builder.Start<_003CDeleteProductAsync_003Ed__7>(ref _003CDeleteProductAsync_003Ed__);
		return _003CDeleteProductAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetCategoriesAsync_003Ed__8))]
	public System.Threading.Tasks.Task<List<CategoryDTO>> GetCategoriesAsync()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetCategoriesAsync_003Ed__8 _003CGetCategoriesAsync_003Ed__ = default(_003CGetCategoriesAsync_003Ed__8);
		_003CGetCategoriesAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<List<CategoryDTO>>.Create();
		_003CGetCategoriesAsync_003Ed__._003C_003E4__this = this;
		_003CGetCategoriesAsync_003Ed__._003C_003E1__state = -1;
		_003CGetCategoriesAsync_003Ed__._003C_003Et__builder.Start<_003CGetCategoriesAsync_003Ed__8>(ref _003CGetCategoriesAsync_003Ed__);
		return _003CGetCategoriesAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateCategoryAsync_003Ed__9))]
	public System.Threading.Tasks.Task<CategoryDTO> CreateCategoryAsync(CreateCategoryRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateCategoryAsync_003Ed__9 _003CCreateCategoryAsync_003Ed__ = default(_003CCreateCategoryAsync_003Ed__9);
		_003CCreateCategoryAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<CategoryDTO>.Create();
		_003CCreateCategoryAsync_003Ed__._003C_003E4__this = this;
		_003CCreateCategoryAsync_003Ed__.request = request;
		_003CCreateCategoryAsync_003Ed__._003C_003E1__state = -1;
		_003CCreateCategoryAsync_003Ed__._003C_003Et__builder.Start<_003CCreateCategoryAsync_003Ed__9>(ref _003CCreateCategoryAsync_003Ed__);
		return _003CCreateCategoryAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetSuppliersAsync_003Ed__10))]
	public System.Threading.Tasks.Task<List<SupplierDTO>> GetSuppliersAsync()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetSuppliersAsync_003Ed__10 _003CGetSuppliersAsync_003Ed__ = default(_003CGetSuppliersAsync_003Ed__10);
		_003CGetSuppliersAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<List<SupplierDTO>>.Create();
		_003CGetSuppliersAsync_003Ed__._003C_003E4__this = this;
		_003CGetSuppliersAsync_003Ed__._003C_003E1__state = -1;
		_003CGetSuppliersAsync_003Ed__._003C_003Et__builder.Start<_003CGetSuppliersAsync_003Ed__10>(ref _003CGetSuppliersAsync_003Ed__);
		return _003CGetSuppliersAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateSupplierAsync_003Ed__11))]
	public System.Threading.Tasks.Task<SupplierDTO> CreateSupplierAsync(CreateSupplierRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateSupplierAsync_003Ed__11 _003CCreateSupplierAsync_003Ed__ = default(_003CCreateSupplierAsync_003Ed__11);
		_003CCreateSupplierAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<SupplierDTO>.Create();
		_003CCreateSupplierAsync_003Ed__._003C_003E4__this = this;
		_003CCreateSupplierAsync_003Ed__.request = request;
		_003CCreateSupplierAsync_003Ed__._003C_003E1__state = -1;
		_003CCreateSupplierAsync_003Ed__._003C_003Et__builder.Start<_003CCreateSupplierAsync_003Ed__11>(ref _003CCreateSupplierAsync_003Ed__);
		return _003CCreateSupplierAsync_003Ed__._003C_003Et__builder.get_Task();
	}
}
