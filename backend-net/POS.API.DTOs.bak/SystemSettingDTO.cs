using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class SystemSettingDTO
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

	public string Key
	{
		[CompilerGenerated]
		get
		{
			return _003CKey_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CKey_003Ek__BackingField = value;
		}
	}

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

	public string? Description
	{
		[CompilerGenerated]
		get
		{
			return _003CDescription_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CDescription_003Ek__BackingField = value;
		}
	}

	public SystemSettingDTO()
	{
		_003CKey_003Ek__BackingField = string.Empty;
		_003CValue_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
