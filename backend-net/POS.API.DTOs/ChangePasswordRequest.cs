using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class ChangePasswordRequest
{
	public string CurrentPassword
	{
		[CompilerGenerated]
		get
		{
			return _003CCurrentPassword_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCurrentPassword_003Ek__BackingField = value;
		}
	}

	public string NewPassword
	{
		[CompilerGenerated]
		get
		{
			return _003CNewPassword_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CNewPassword_003Ek__BackingField = value;
		}
	}

	public ChangePasswordRequest()
	{
		_003CCurrentPassword_003Ek__BackingField = string.Empty;
		_003CNewPassword_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
