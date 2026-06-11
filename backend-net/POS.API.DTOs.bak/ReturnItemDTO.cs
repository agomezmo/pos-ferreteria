using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class ReturnItemDTO
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

	public string ProductName
	{
		[CompilerGenerated]
		get
		{
			return _003CProductName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CProductName_003Ek__BackingField = value;
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

	public ReturnItemDTO()
	{
		_003CProductName_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
