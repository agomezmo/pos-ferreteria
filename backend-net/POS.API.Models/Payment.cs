using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("Payments")]
public class Payment
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

	public int SaleId
	{
		[CompilerGenerated]
		get
		{
			return _003CSaleId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSaleId_003Ek__BackingField = value;
		}
	}

	[ForeignKey("SaleId")]
	public Sale Sale
	{
		[CompilerGenerated]
		get
		{
			return _003CSale_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSale_003Ek__BackingField = value;
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

	[Required]
	[StringLength(30)]
	public string PaymentMethod
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

	public Payment()
	{
		_003CPaymentMethod_003Ek__BackingField = string.Empty;
		_003CCreatedAt_003Ek__BackingField = System.DateTime.get_UtcNow();
		base._002Ector();
	}
}
