using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("CashRegisters")]
public class CashRegister
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

	[StringLength(200)]
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

	public System.Collections.Generic.ICollection<CashRegisterSession> Sessions
	{
		[CompilerGenerated]
		get
		{
			return _003CSessions_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSessions_003Ek__BackingField = value;
		}
	}

	public CashRegister()
	{
		_003CName_003Ek__BackingField = string.Empty;
		_003CIsActive_003Ek__BackingField = true;
		_003CCreatedAt_003Ek__BackingField = System.DateTime.get_UtcNow();
		_003CSessions_003Ek__BackingField = (System.Collections.Generic.ICollection<CashRegisterSession>)new List<CashRegisterSession>();
		base._002Ector();
	}
}
