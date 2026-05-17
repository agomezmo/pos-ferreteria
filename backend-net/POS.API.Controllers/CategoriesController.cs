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
public class CategoriesController : ControllerBase
{
	private readonly AppDbContext _context;

	public CategoriesController(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CGetCategoryProductCount_003Ed__2))]
	[HttpGet("product-count")]
	public System.Threading.Tasks.Task<ActionResult<List<CategoryProductCountDTO>>> GetCategoryProductCount()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetCategoryProductCount_003Ed__2 _003CGetCategoryProductCount_003Ed__ = default(_003CGetCategoryProductCount_003Ed__2);
		_003CGetCategoryProductCount_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<CategoryProductCountDTO>>>.Create();
		_003CGetCategoryProductCount_003Ed__._003C_003E4__this = this;
		_003CGetCategoryProductCount_003Ed__._003C_003E1__state = -1;
		_003CGetCategoryProductCount_003Ed__._003C_003Et__builder.Start<_003CGetCategoryProductCount_003Ed__2>(ref _003CGetCategoryProductCount_003Ed__);
		return _003CGetCategoryProductCount_003Ed__._003C_003Et__builder.get_Task();
	}
}
