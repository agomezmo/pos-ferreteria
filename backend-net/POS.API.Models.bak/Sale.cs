using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("Sales")]
public class Sale
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
	[StringLength(20)]
	public string ReceiptNumber
	{
		[CompilerGenerated]
		get
		{
			return _003CReceiptNumber_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CReceiptNumber_003Ek__BackingField = value;
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

	public int? CustomerId
	{
		[CompilerGenerated]
		get
		{
			return _003CCustomerId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCustomerId_003Ek__BackingField = value;
		}
	}

	[ForeignKey("CustomerId")]
	public Customer? Customer
	{
		[CompilerGenerated]
		get
		{
			return _003CCustomer_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCustomer_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal Subtotal
	{
		[CompilerGenerated]
		get
		{
			return _003CSubtotal_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSubtotal_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal Tax
	{
		[CompilerGenerated]
		get
		{
			return _003CTax_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CTax_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal Discount
	{
		[CompilerGenerated]
		get
		{
			return _003CDiscount_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CDiscount_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal Total
	{
		[CompilerGenerated]
		get
		{
			return _003CTotal_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CTotal_003Ek__BackingField = value;
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

	[Required]
	[StringLength(20)]
	public string PaymentStatus
	{
		[CompilerGenerated]
		get
		{
			return _003CPaymentStatus_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CPaymentStatus_003Ek__BackingField = value;
		}
	}

	[Column("sale_type")]
	[StringLength(20)]
	public string SaleType
	{
		[CompilerGenerated]
		get
		{
			return _003CSaleType_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSaleType_003Ek__BackingField = value;
		}
	}

	[StringLength(500)]
	public string? Notes
	{
		[CompilerGenerated]
		get
		{
			return _003CNotes_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CNotes_003Ek__BackingField = value;
		}
	}

	public int? CashRegisterSessionId
	{
		[CompilerGenerated]
		get
		{
			return _003CCashRegisterSessionId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCashRegisterSessionId_003Ek__BackingField = value;
		}
	}

	[ForeignKey("CashRegisterSessionId")]
	public CashRegisterSession? CashRegisterSession
	{
		[CompilerGenerated]
		get
		{
			return _003CCashRegisterSession_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCashRegisterSession_003Ek__BackingField = value;
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

	public System.Collections.Generic.ICollection<SaleItem> SaleItems
	{
		[CompilerGenerated]
		get
		{
			return _003CSaleItems_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSaleItems_003Ek__BackingField = value;
		}
	}

	public System.Collections.Generic.ICollection<Payment> Payments
	{
		[CompilerGenerated]
		get
		{
			return _003CPayments_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CPayments_003Ek__BackingField = value;
		}
	}

	public System.Collections.Generic.ICollection<Return> Returns
	{
		[CompilerGenerated]
		get
		{
			return _003CReturns_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CReturns_003Ek__BackingField = value;
		}
	}

	public Sale()
	{
		_003CReceiptNumber_003Ek__BackingField = string.Empty;
		_003CPaymentMethod_003Ek__BackingField = string.Empty;
		_003CPaymentStatus_003Ek__BackingField = "Completed";
		_003CSaleType_003Ek__BackingField = "Cash";
		_003CCreatedAt_003Ek__BackingField = System.DateTime.get_UtcNow();
		_003CSaleItems_003Ek__BackingField = (System.Collections.Generic.ICollection<SaleItem>)new List<SaleItem>();
		_003CPayments_003Ek__BackingField = (System.Collections.Generic.ICollection<Payment>)new List<Payment>();
		_003CReturns_003Ek__BackingField = (System.Collections.Generic.ICollection<Return>)new List<Return>();
		base._002Ector();
	}
}
