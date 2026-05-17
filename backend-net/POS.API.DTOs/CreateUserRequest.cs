using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class CreateUserRequest
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

	public string FullName
	{
		[CompilerGenerated]
		get
		{
			return _003CFullName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CFullName_003Ek__BackingField = value;
		}
	}

	public string? Email
	{
		[CompilerGenerated]
		get
		{
			return _003CEmail_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CEmail_003Ek__BackingField = value;
		}
	}

	public int RoleId
	{
		[CompilerGenerated]
		get
		{
			return _003CRoleId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRoleId_003Ek__BackingField = value;
		}
	}

	public CreateUserRequest()
	{
		_003CUsername_003Ek__BackingField = string.Empty;
		_003CPassword_003Ek__BackingField = string.Empty;
		_003CFullName_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
