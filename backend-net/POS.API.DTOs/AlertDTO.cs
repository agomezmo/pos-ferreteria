using System;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class AlertDTO
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

	public string Type
	{
		[CompilerGenerated]
		get
		{
			return _003CType_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CType_003Ek__BackingField = value;
		}
	}

	public string Title
	{
		[CompilerGenerated]
		get
		{
			return _003CTitle_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CTitle_003Ek__BackingField = value;
		}
	}

	public string? Message
	{
		[CompilerGenerated]
		get
		{
			return _003CMessage_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CMessage_003Ek__BackingField = value;
		}
	}

	public bool IsRead
	{
		[CompilerGenerated]
		get
		{
			return _003CIsRead_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIsRead_003Ek__BackingField = value;
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

	public AlertDTO()
	{
		_003CType_003Ek__BackingField = string.Empty;
		_003CTitle_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
