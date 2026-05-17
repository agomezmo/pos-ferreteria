using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("CashRegisterSessions")]
public class CashRegisterSession
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

	public int CashRegisterId
	{
		[CompilerGenerated]
		get
		{
			return _003CCashRegisterId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCashRegisterId_003Ek__BackingField = value;
		}
	}

	[ForeignKey("CashRegisterId")]
	public CashRegister CashRegister
	{
		[CompilerGenerated]
		get
		{
			return _003CCashRegister_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCashRegister_003Ek__BackingField = value;
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

	[ForeignKey("UserId")]
	public User User
	{
		[CompilerGenerated]
		get
		{
			return _003CUser_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUser_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal OpeningAmount
	{
		[CompilerGenerated]
		get
		{
			return _003COpeningAmount_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003COpeningAmount_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal? ClosingAmount
	{
		[CompilerGenerated]
		get
		{
			return _003CClosingAmount_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CClosingAmount_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal ExpectedAmount
	{
		[CompilerGenerated]
		get
		{
			return _003CExpectedAmount_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CExpectedAmount_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal Difference
	{
		[CompilerGenerated]
		get
		{
			return _003CDifference_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CDifference_003Ek__BackingField = value;
		}
	}

	[StringLength(200)]
	public string? OpeningNotes
	{
		[CompilerGenerated]
		get
		{
			return _003COpeningNotes_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003COpeningNotes_003Ek__BackingField = value;
		}
	}

	[StringLength(200)]
	public string? ClosingNotes
	{
		[CompilerGenerated]
		get
		{
			return _003CClosingNotes_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CClosingNotes_003Ek__BackingField = value;
		}
	}

	public System.DateTime OpenedAt
	{
		[CompilerGenerated]
		get
		{
			return _003COpenedAt_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003COpenedAt_003Ek__BackingField = value;
		}
	}

	public System.DateTime? ClosedAt
	{
		[CompilerGenerated]
		get
		{
			return _003CClosedAt_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CClosedAt_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(20)]
	public string Status
	{
		[CompilerGenerated]
		get
		{
			return _003CStatus_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CStatus_003Ek__BackingField = value;
		}
	}

	public System.Collections.Generic.ICollection<Sale> Sales
	{
		[CompilerGenerated]
		get
		{
			return _003CSales_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSales_003Ek__BackingField = value;
		}
	}

	public CashRegisterSession()
	{
		_003COpenedAt_003Ek__BackingField = System.DateTime.get_UtcNow();
		_003CStatus_003Ek__BackingField = "Open";
		_003CSales_003Ek__BackingField = (System.Collections.Generic.ICollection<Sale>)new List<Sale>();
		base._002Ector();
	}
}
