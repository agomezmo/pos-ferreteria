using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class CreateSaleRequest
{
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

	public List<CreateSaleItemRequest> Items
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

	public CreateSaleRequest()
	{
		_003CPaymentMethod_003Ek__BackingField = "Cash";
		_003CSaleType_003Ek__BackingField = "Cash";
		_003CItems_003Ek__BackingField = new List<CreateSaleItemRequest>();
		base._002Ector();
	}
}
