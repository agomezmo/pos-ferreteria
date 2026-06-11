using System;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class LoginResponse
{
	public string Token
	{
		[CompilerGenerated]
		get
		{
			return _003CToken_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CToken_003Ek__BackingField = value;
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

	public string Role
	{
		[CompilerGenerated]
		get
		{
			return _003CRole_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRole_003Ek__BackingField = value;
		}
	}

	public int UserId
	{
		[CompilerGenerated]
		get
		{
			return _003CUserId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUserId_003Ek__BackingField = value;
		}
	}

	public System.DateTime ExpiresAt
	{
		[CompilerGenerated]
		get
		{
			return _003CExpiresAt_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CExpiresAt_003Ek__BackingField = value;
		}
	}

	public LoginResponse()
	{
		_003CToken_003Ek__BackingField = string.Empty;
		_003CFullName_003Ek__BackingField = string.Empty;
		_003CRole_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
