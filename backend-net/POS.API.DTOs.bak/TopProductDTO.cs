using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class TopProductDTO
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

	public string ProductCode
	{
		[CompilerGenerated]
		get
		{
			return _003CProductCode_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CProductCode_003Ek__BackingField = value;
		}
	}

	public string CategoryName
	{
		[CompilerGenerated]
		get
		{
			return _003CCategoryName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCategoryName_003Ek__BackingField = value;
		}
	}

	public int TotalQuantity
	{
		[CompilerGenerated]
		get
		{
			return _003CTotalQuantity_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CTotalQuantity_003Ek__BackingField = value;
		}
	}

	public decimal TotalRevenue
	{
		[CompilerGenerated]
		get
		{
			return _003CTotalRevenue_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CTotalRevenue_003Ek__BackingField = value;
		}
	}

	public TopProductDTO()
	{
		_003CProductName_003Ek__BackingField = string.Empty;
		_003CProductCode_003Ek__BackingField = string.Empty;
		_003CCategoryName_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
