using System;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class SaleListDTO
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

	public SaleListDTO()
	{
		_003CReceiptNumber_003Ek__BackingField = string.Empty;
		_003CUserName_003Ek__BackingField = string.Empty;
		_003CPaymentMethod_003Ek__BackingField = string.Empty;
		_003CPaymentStatus_003Ek__BackingField = string.Empty;
		_003CSaleType_003Ek__BackingField = "Cash";
		base._002Ector();
	}
}
