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
public class ProductsController : ControllerBase
{
	private readonly ProductService _productService;

	public ProductsController(ProductService productService)
	{
		_productService = productService;
	}

	[AsyncStateMachine(typeof(_003CGetProducts_003Ed__2))]
	[HttpGet]
	public System.Threading.Tasks.Task<ActionResult<List<ProductDTO>>> GetProducts([FromQuery] bool includeInactive = false)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetProducts_003Ed__2 _003CGetProducts_003Ed__ = default(_003CGetProducts_003Ed__2);
		_003CGetProducts_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<ProductDTO>>>.Create();
		_003CGetProducts_003Ed__._003C_003E4__this = this;
		_003CGetProducts_003Ed__.includeInactive = includeInactive;
		_003CGetProducts_003Ed__._003C_003E1__state = -1;
		_003CGetProducts_003Ed__._003C_003Et__builder.Start<_003CGetProducts_003Ed__2>(ref _003CGetProducts_003Ed__);
		return _003CGetProducts_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetProduct_003Ed__3))]
	[HttpGet("{id}")]
	public System.Threading.Tasks.Task<ActionResult<ProductDTO>> GetProduct(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetProduct_003Ed__3 _003CGetProduct_003Ed__ = default(_003CGetProduct_003Ed__3);
		_003CGetProduct_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<ProductDTO>>.Create();
		_003CGetProduct_003Ed__._003C_003E4__this = this;
		_003CGetProduct_003Ed__.id = id;
		_003CGetProduct_003Ed__._003C_003E1__state = -1;
		_003CGetProduct_003Ed__._003C_003Et__builder.Start<_003CGetProduct_003Ed__3>(ref _003CGetProduct_003Ed__);
		return _003CGetProduct_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CSearchProducts_003Ed__4))]
	[HttpGet("search")]
	public System.Threading.Tasks.Task<ActionResult<List<ProductDTO>>> SearchProducts([FromQuery] string q)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CSearchProducts_003Ed__4 _003CSearchProducts_003Ed__ = default(_003CSearchProducts_003Ed__4);
		_003CSearchProducts_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<ProductDTO>>>.Create();
		_003CSearchProducts_003Ed__._003C_003E4__this = this;
		_003CSearchProducts_003Ed__.q = q;
		_003CSearchProducts_003Ed__._003C_003E1__state = -1;
		_003CSearchProducts_003Ed__._003C_003Et__builder.Start<_003CSearchProducts_003Ed__4>(ref _003CSearchProducts_003Ed__);
		return _003CSearchProducts_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateProduct_003Ed__5))]
	[HttpPost]
	public System.Threading.Tasks.Task<ActionResult<ProductDTO>> CreateProduct([FromBody] CreateProductRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateProduct_003Ed__5 _003CCreateProduct_003Ed__ = default(_003CCreateProduct_003Ed__5);
		_003CCreateProduct_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<ProductDTO>>.Create();
		_003CCreateProduct_003Ed__._003C_003E4__this = this;
		_003CCreateProduct_003Ed__.request = request;
		_003CCreateProduct_003Ed__._003C_003E1__state = -1;
		_003CCreateProduct_003Ed__._003C_003Et__builder.Start<_003CCreateProduct_003Ed__5>(ref _003CCreateProduct_003Ed__);
		return _003CCreateProduct_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CUpdateProduct_003Ed__6))]
	[HttpPut("{id}")]
	public System.Threading.Tasks.Task<ActionResult<ProductDTO>> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CUpdateProduct_003Ed__6 _003CUpdateProduct_003Ed__ = default(_003CUpdateProduct_003Ed__6);
		_003CUpdateProduct_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<ProductDTO>>.Create();
		_003CUpdateProduct_003Ed__._003C_003E4__this = this;
		_003CUpdateProduct_003Ed__.id = id;
		_003CUpdateProduct_003Ed__.request = request;
		_003CUpdateProduct_003Ed__._003C_003E1__state = -1;
		_003CUpdateProduct_003Ed__._003C_003Et__builder.Start<_003CUpdateProduct_003Ed__6>(ref _003CUpdateProduct_003Ed__);
		return _003CUpdateProduct_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CDeleteProduct_003Ed__7))]
	[HttpDelete("{id}")]
	public System.Threading.Tasks.Task<ActionResult> DeleteProduct(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CDeleteProduct_003Ed__7 _003CDeleteProduct_003Ed__ = default(_003CDeleteProduct_003Ed__7);
		_003CDeleteProduct_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult>.Create();
		_003CDeleteProduct_003Ed__._003C_003E4__this = this;
		_003CDeleteProduct_003Ed__.id = id;
		_003CDeleteProduct_003Ed__._003C_003E1__state = -1;
		_003CDeleteProduct_003Ed__._003C_003Et__builder.Start<_003CDeleteProduct_003Ed__7>(ref _003CDeleteProduct_003Ed__);
		return _003CDeleteProduct_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetCategories_003Ed__8))]
	[HttpGet("categories")]
	public System.Threading.Tasks.Task<ActionResult<List<CategoryDTO>>> GetCategories()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetCategories_003Ed__8 _003CGetCategories_003Ed__ = default(_003CGetCategories_003Ed__8);
		_003CGetCategories_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<CategoryDTO>>>.Create();
		_003CGetCategories_003Ed__._003C_003E4__this = this;
		_003CGetCategories_003Ed__._003C_003E1__state = -1;
		_003CGetCategories_003Ed__._003C_003Et__builder.Start<_003CGetCategories_003Ed__8>(ref _003CGetCategories_003Ed__);
		return _003CGetCategories_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateCategory_003Ed__9))]
	[HttpPost("categories")]
	public System.Threading.Tasks.Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CreateCategoryRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateCategory_003Ed__9 _003CCreateCategory_003Ed__ = default(_003CCreateCategory_003Ed__9);
		_003CCreateCategory_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<CategoryDTO>>.Create();
		_003CCreateCategory_003Ed__._003C_003E4__this = this;
		_003CCreateCategory_003Ed__.request = request;
		_003CCreateCategory_003Ed__._003C_003E1__state = -1;
		_003CCreateCategory_003Ed__._003C_003Et__builder.Start<_003CCreateCategory_003Ed__9>(ref _003CCreateCategory_003Ed__);
		return _003CCreateCategory_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetSuppliers_003Ed__10))]
	[HttpGet("suppliers")]
	public System.Threading.Tasks.Task<ActionResult<List<SupplierDTO>>> GetSuppliers()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetSuppliers_003Ed__10 _003CGetSuppliers_003Ed__ = default(_003CGetSuppliers_003Ed__10);
		_003CGetSuppliers_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<SupplierDTO>>>.Create();
		_003CGetSuppliers_003Ed__._003C_003E4__this = this;
		_003CGetSuppliers_003Ed__._003C_003E1__state = -1;
		_003CGetSuppliers_003Ed__._003C_003Et__builder.Start<_003CGetSuppliers_003Ed__10>(ref _003CGetSuppliers_003Ed__);
		return _003CGetSuppliers_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateSupplier_003Ed__11))]
	[HttpPost("suppliers")]
	public System.Threading.Tasks.Task<ActionResult<SupplierDTO>> CreateSupplier([FromBody] CreateSupplierRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateSupplier_003Ed__11 _003CCreateSupplier_003Ed__ = default(_003CCreateSupplier_003Ed__11);
		_003CCreateSupplier_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<SupplierDTO>>.Create();
		_003CCreateSupplier_003Ed__._003C_003E4__this = this;
		_003CCreateSupplier_003Ed__.request = request;
		_003CCreateSupplier_003Ed__._003C_003E1__state = -1;
		_003CCreateSupplier_003Ed__._003C_003Et__builder.Start<_003CCreateSupplier_003Ed__11>(ref _003CCreateSupplier_003Ed__);
		return _003CCreateSupplier_003Ed__._003C_003Et__builder.get_Task();
	}
}
