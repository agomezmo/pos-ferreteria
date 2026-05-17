using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class InventoryReportDTO
{
	public int TotalProducts
	{
		[CompilerGenerated]
		get
		{
			return _003CTotalProducts_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CTotalProducts_003Ek__BackingField = value;
		}
	}

	public int LowStockProducts
	{
		[CompilerGenerated]
		get
		{
			return _003CLowStockProducts_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CLowStockProducts_003Ek__BackingField = value;
		}
	}

	public int OutOfStockProducts
	{
		[CompilerGenerated]
		get
		{
			return _003COutOfStockProducts_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003COutOfStockProducts_003Ek__BackingField = value;
		}
	}

	public decimal TotalInventoryValue
	{
		[CompilerGenerated]
		get
		{
			return _003CTotalInventoryValue_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CTotalInventoryValue_003Ek__BackingField = value;
		}
	}

	public List<ProductDTO> LowStockItems
	{
		[CompilerGenerated]
		get
		{
			return _003CLowStockItems_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CLowStockItems_003Ek__BackingField = value;
		}
	}

	public InventoryReportDTO()
	{
		_003CLowStockItems_003Ek__BackingField = new List<ProductDTO>();
		base._002Ector();
	}
}
