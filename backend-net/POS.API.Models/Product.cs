using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("Products")]
public class Product
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
	[StringLength(50)]
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

	[StringLength(50)]
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

	[Required]
	[StringLength(200)]
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

	[StringLength(500)]
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

	[ForeignKey("CategoryId")]
	public Category Category
	{
		[CompilerGenerated]
		get
		{
			return _003CCategory_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCategory_003Ek__BackingField = value;
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

	[ForeignKey("SupplierId")]
	public Supplier? Supplier
	{
		[CompilerGenerated]
		get
		{
			return _003CSupplier_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSupplier_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
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

	[Column(TypeName = "decimal(18,2)")]
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

	[Column("wholesale_price", TypeName = "decimal(18,2)")]
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

	[Column(TypeName = "decimal(18,2)")]
	public decimal Stock
	{
		[CompilerGenerated]
		get
		{
			return _003CStock_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CStock_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
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

	[StringLength(20)]
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

	[Column("requires_tax")]
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

	[Column("is_service")]
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

	public System.DateTime? UpdatedAt
	{
		[CompilerGenerated]
		get
		{
			return _003CUpdatedAt_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUpdatedAt_003Ek__BackingField = value;
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

	public System.Collections.Generic.ICollection<InventoryMovement> InventoryMovements
	{
		[CompilerGenerated]
		get
		{
			return _003CInventoryMovements_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CInventoryMovements_003Ek__BackingField = value;
		}
	}

	public System.Collections.Generic.ICollection<ReturnItem> ReturnItems
	{
		[CompilerGenerated]
		get
		{
			return _003CReturnItems_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CReturnItems_003Ek__BackingField = value;
		}
	}

	public Product()
	{
		_003CCode_003Ek__BackingField = string.Empty;
		_003CName_003Ek__BackingField = string.Empty;
		_003CUnit_003Ek__BackingField = "pza";
		_003CIsActive_003Ek__BackingField = true;
		_003CRequiresTax_003Ek__BackingField = true;
		_003CCreatedAt_003Ek__BackingField = System.DateTime.get_UtcNow();
		_003CSaleItems_003Ek__BackingField = (System.Collections.Generic.ICollection<SaleItem>)new List<SaleItem>();
		_003CInventoryMovements_003Ek__BackingField = (System.Collections.Generic.ICollection<InventoryMovement>)new List<InventoryMovement>();
		_003CReturnItems_003Ek__BackingField = (System.Collections.Generic.ICollection<ReturnItem>)new List<ReturnItem>();
		base._002Ector();
	}
}
