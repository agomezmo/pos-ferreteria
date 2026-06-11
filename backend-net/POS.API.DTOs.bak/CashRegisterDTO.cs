using System;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class CashRegisterDTO
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

	public string Name
	{
		[CompilerGenerated]
		get
		{
			return _003CName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CName_003Ek__BackingField = value;
		}
	}

	public string? Location
	{
		[CompilerGenerated]
		get
		{
			return _003CLocation_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CLocation_003Ek__BackingField = value;
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

	public CashRegisterDTO()
	{
		_003CName_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
