using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class UpdateSystemSettingRequest
{
	public string Value
	{
		[CompilerGenerated]
		get
		{
			return _003CValue_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CValue_003Ek__BackingField = value;
		}
	}

	public UpdateSystemSettingRequest()
	{
		_003CValue_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
