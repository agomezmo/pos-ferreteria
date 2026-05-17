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
public class CompanyController : ControllerBase
{
	private readonly AppDbContext _context;

	public CompanyController(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CGetCompanyInfo_003Ed__2))]
	[HttpGet]
	public System.Threading.Tasks.Task<ActionResult<CompanyInfoDTO>> GetCompanyInfo()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetCompanyInfo_003Ed__2 _003CGetCompanyInfo_003Ed__ = default(_003CGetCompanyInfo_003Ed__2);
		_003CGetCompanyInfo_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<CompanyInfoDTO>>.Create();
		_003CGetCompanyInfo_003Ed__._003C_003E4__this = this;
		_003CGetCompanyInfo_003Ed__._003C_003E1__state = -1;
		_003CGetCompanyInfo_003Ed__._003C_003Et__builder.Start<_003CGetCompanyInfo_003Ed__2>(ref _003CGetCompanyInfo_003Ed__);
		return _003CGetCompanyInfo_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CUpdateCompanyInfo_003Ed__3))]
	[HttpPut]
	public System.Threading.Tasks.Task<ActionResult<CompanyInfoDTO>> UpdateCompanyInfo([FromBody] UpdateCompanyInfoRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CUpdateCompanyInfo_003Ed__3 _003CUpdateCompanyInfo_003Ed__ = default(_003CUpdateCompanyInfo_003Ed__3);
		_003CUpdateCompanyInfo_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<CompanyInfoDTO>>.Create();
		_003CUpdateCompanyInfo_003Ed__._003C_003E4__this = this;
		_003CUpdateCompanyInfo_003Ed__.request = request;
		_003CUpdateCompanyInfo_003Ed__._003C_003E1__state = -1;
		_003CUpdateCompanyInfo_003Ed__._003C_003Et__builder.Start<_003CUpdateCompanyInfo_003Ed__3>(ref _003CUpdateCompanyInfo_003Ed__);
		return _003CUpdateCompanyInfo_003Ed__._003C_003Et__builder.get_Task();
	}
}
