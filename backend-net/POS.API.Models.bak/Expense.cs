using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("Expenses")]
public class Expense
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
	public string Category
	{
		[CompilerGenerated]
		get
		{
			return _003CCategory_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCategory_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(500)]
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

	[Column(TypeName = "decimal(18,2)")]
	public decimal Amount
	{
		[CompilerGenerated]
		get
		{
			return _003CAmount_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CAmount_003Ek__BackingField = value;
		}
	}

	[StringLength(30)]
	public string? PaymentMethod
	{
		[CompilerGenerated]
		get
		{
			return _003CPaymentMethod_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CPaymentMethod_003Ek__BackingField = value;
		}
	}

	[StringLength(100)]
	public string? Reference
	{
		[CompilerGenerated]
		get
		{
			return _003CReference_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CReference_003Ek__BackingField = value;
		}
	}

	public int? UserId
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
	public User? User
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

	public Expense()
	{
		_003CCategory_003Ek__BackingField = string.Empty;
		_003CDescription_003Ek__BackingField = string.Empty;
		_003CCreatedAt_003Ek__BackingField = System.DateTime.get_UtcNow();
		base._002Ector();
	}
}
