using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class UpdateUserRequest
{
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

	public bool IsActive
	{
		[CompilerGenerated]
		get
		{
			return _003CIsActive_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIsActive_003Ek__BackingField = value;
		}
	}

	public UpdateUserRequest()
	{
		_003CFullName_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
