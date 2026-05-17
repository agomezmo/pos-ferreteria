using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("CatFormasPago")]
public class CatFormaPago
{
	[Key]
	[StringLength(10)]
	public string Id
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
	[StringLength(200)]
	public string Description
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

	public CatFormaPago()
	{
		_003CId_003Ek__BackingField = string.Empty;
		_003CDescription_003Ek__BackingField = string.Empty;
		_003CIsActive_003Ek__BackingField = true;
		base._002Ector();
	}
}
