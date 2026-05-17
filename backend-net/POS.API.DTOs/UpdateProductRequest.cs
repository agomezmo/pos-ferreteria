using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class UpdateProductRequest
{
	public string Code
	{
		[CompilerGenerated]
		get
		{
			return _003CCode_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCode_003Ek__BackingField = value;
		}
	}

	public string? Barcode
	{
		[CompilerGenerated]
		get
		{
			return _003CBarcode_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CBarcode_003Ek__BackingField = value;
		}
	}

	public string Name
	{
		[CompilerGenerated]
		get
		{
			return _003CName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CName_003Ek__BackingField = value;
		}
	}

	public string? Description
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

	public int CategoryId
	{
		[CompilerGenerated]
		get
		{
			return _003CCategoryId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCategoryId_003Ek__BackingField = value;
		}
	}

	public int? SupplierId
	{
		[CompilerGenerated]
		get
		{
			return _003CSupplierId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSupplierId_003Ek__BackingField = value;
		}
	}

	public decimal PurchasePrice
	{
		[CompilerGenerated]
		get
		{
			return _003CPurchasePrice_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CPurchasePrice_003Ek__BackingField = value;
		}
	}

	public decimal SalePrice
	{
		[CompilerGenerated]
		get
		{
			return _003CSalePrice_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSalePrice_003Ek__BackingField = value;
		}
	}

	public decimal WholesalePrice
	{
		[CompilerGenerated]
		get
		{
			return _003CWholesalePrice_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CWholesalePrice_003Ek__BackingField = value;
		}
	}

	public decimal MinStock
	{
		[CompilerGenerated]
		get
		{
			return _003CMinStock_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CMinStock_003Ek__BackingField = value;
		}
	}

	public string Unit
	{
		[CompilerGenerated]
		get
		{
			return _003CUnit_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUnit_003Ek__BackingField = value;
		}
	}

	public bool IsActive
	{
		[CompilerGenerated]
		get
		{
			return _003CIsActive_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIsActive_003Ek__BackingField = value;
		}
	}

	public bool RequiresTax
	{
		[CompilerGenerated]
		get
		{
			return _003CRequiresTax_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRequiresTax_003Ek__BackingField = value;
		}
	}

	public bool IsService
	{
		[CompilerGenerated]
		get
		{
			return _003CIsService_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIsService_003Ek__BackingField = value;
		}
	}

	public UpdateProductRequest()
	{
		_003CCode_003Ek__BackingField = string.Empty;
		_003CName_003Ek__BackingField = string.Empty;
		_003CUnit_003Ek__BackingField = "pza";
		_003CIsActive_003Ek__BackingField = true;
		_003CRequiresTax_003Ek__BackingField = true;
		base._002Ector();
	}
}
