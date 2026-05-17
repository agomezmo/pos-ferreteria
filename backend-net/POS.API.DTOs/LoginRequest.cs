using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class LoginRequest
{
	public string Username
	{
		[CompilerGenerated]
		get
		{
			return _003CUsername_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUsername_003Ek__BackingField = value;
		}
	}

	public string Password
	{
		[CompilerGenerated]
		get
		{
			return _003CPassword_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CPassword_003Ek__BackingField = value;
		}
	}

	public LoginRequest()
	{
		_003CUsername_003Ek__BackingField = string.Empty;
		_003CPassword_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
