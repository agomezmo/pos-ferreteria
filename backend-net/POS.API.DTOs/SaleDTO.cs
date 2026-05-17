using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class SaleDTO
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

	public string UserName
	{
		[CompilerGenerated]
		get
		{
			return _003CUserName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUserName_003Ek__BackingField = value;
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

	public string? CustomerName
	{
		[CompilerGenerated]
		get
		{
			return _003CCustomerName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCustomerName_003Ek__BackingField = value;
		}
	}

	public string? CustomerDocument
	{
		[CompilerGenerated]
		get
		{
			return _003CCustomerDocument_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCustomerDocument_003Ek__BackingField = value;
		}
	}

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

	public List<SaleItemDTO> Items
	{
		[CompilerGenerated]
		get
		{
			return _003CItems_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CItems_003Ek__BackingField = value;
		}
	}

	public SaleDTO()
	{
		_003CReceiptNumber_003Ek__BackingField = string.Empty;
		_003CUserName_003Ek__BackingField = string.Empty;
		_003CPaymentMethod_003Ek__BackingField = string.Empty;
		_003CPaymentStatus_003Ek__BackingField = string.Empty;
		_003CSaleType_003Ek__BackingField = "Cash";
		_003CItems_003Ek__BackingField = new List<SaleItemDTO>();
		base._002Ector();
	}
}
