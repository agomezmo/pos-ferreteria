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
public class SettingsController : ControllerBase
{
	private readonly AppDbContext _context;

	public SettingsController(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CGetSettings_003Ed__2))]
	[HttpGet]
	public System.Threading.Tasks.Task<ActionResult<List<SystemSettingDTO>>> GetSettings()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetSettings_003Ed__2 _003CGetSettings_003Ed__ = default(_003CGetSettings_003Ed__2);
		_003CGetSettings_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<SystemSettingDTO>>>.Create();
		_003CGetSettings_003Ed__._003C_003E4__this = this;
		_003CGetSettings_003Ed__._003C_003E1__state = -1;
		_003CGetSettings_003Ed__._003C_003Et__builder.Start<_003CGetSettings_003Ed__2>(ref _003CGetSettings_003Ed__);
		return _003CGetSettings_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetSetting_003Ed__3))]
	[HttpGet("{key}")]
	public System.Threading.Tasks.Task<ActionResult<SystemSettingDTO>> GetSetting(string key)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetSetting_003Ed__3 _003CGetSetting_003Ed__ = default(_003CGetSetting_003Ed__3);
		_003CGetSetting_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<SystemSettingDTO>>.Create();
		_003CGetSetting_003Ed__._003C_003E4__this = this;
		_003CGetSetting_003Ed__.key = key;
		_003CGetSetting_003Ed__._003C_003E1__state = -1;
		_003CGetSetting_003Ed__._003C_003Et__builder.Start<_003CGetSetting_003Ed__3>(ref _003CGetSetting_003Ed__);
		return _003CGetSetting_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CUpdateSetting_003Ed__4))]
	[HttpPut("{key}")]
	public System.Threading.Tasks.Task<ActionResult<SystemSettingDTO>> UpdateSetting(string key, [FromBody] UpdateSystemSettingRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CUpdateSetting_003Ed__4 _003CUpdateSetting_003Ed__ = default(_003CUpdateSetting_003Ed__4);
		_003CUpdateSetting_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<SystemSettingDTO>>.Create();
		_003CUpdateSetting_003Ed__._003C_003E4__this = this;
		_003CUpdateSetting_003Ed__.key = key;
		_003CUpdateSetting_003Ed__.request = request;
		_003CUpdateSetting_003Ed__._003C_003E1__state = -1;
		_003CUpdateSetting_003Ed__._003C_003Et__builder.Start<_003CUpdateSetting_003Ed__4>(ref _003CUpdateSetting_003Ed__);
		return _003CUpdateSetting_003Ed__._003C_003Et__builder.get_Task();
	}
}
