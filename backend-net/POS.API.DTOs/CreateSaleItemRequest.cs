using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class CreateSaleItemRequest
{
	public int ProductId
	{
		[CompilerGenerated]
		get
		{
			return _003CProductId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CProductId_003Ek__BackingField = value;
		}
	}

	public decimal Quantity
	{
		[CompilerGenerated]
		get
		{
			return _003CQuantity_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CQuantity_003Ek__BackingField = value;
		}
	}

	public decimal UnitPrice
	{
		[CompilerGenerated]
		get
		{
			return _003CUnitPrice_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUnitPrice_003Ek__BackingField = value;
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
}
