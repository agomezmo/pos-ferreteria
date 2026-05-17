using System;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class UserDTO
{
	public int Id
	{
		[CompilerGenerated]
		get
		{
			return _003CId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CId_003Ek__BackingField = value;
		}
	}

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

	public string RoleName
	{
		[CompilerGenerated]
		get
		{
			return _003CRoleName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRoleName_003Ek__BackingField = value;
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

	public System.DateTime? LastLogin
	{
		[CompilerGenerated]
		get
		{
			return _003CLastLogin_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CLastLogin_003Ek__BackingField = value;
		}
	}

	public System.DateTime CreatedAt
	{
		[CompilerGenerated]
		get
		{
			return _003CCreatedAt_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCreatedAt_003Ek__BackingField = value;
		}
	}

	public UserDTO()
	{
		_003CUsername_003Ek__BackingField = string.Empty;
		_003CFullName_003Ek__BackingField = string.Empty;
		_003CRoleName_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
