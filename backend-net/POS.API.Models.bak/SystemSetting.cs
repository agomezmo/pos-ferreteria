using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("SystemSettings")]
public class SystemSetting
{
	[Key]
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

	[Required]
	[StringLength(100)]
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

	[Required]
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

	[StringLength(200)]
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

	public SystemSetting()
	{
		_003CKey_003Ek__BackingField = string.Empty;
		_003CValue_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
