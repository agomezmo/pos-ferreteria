using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("ReturnItems")]
public class ReturnItem
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

	public int ReturnId
	{
		[CompilerGenerated]
		get
		{
			return _003CReturnId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CReturnId_003Ek__BackingField = value;
		}
	}

	[ForeignKey("ReturnId")]
	public Return Return
	{
		[CompilerGenerated]
		get
		{
			return _003CReturn_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CReturn_003Ek__BackingField = value;
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

	[ForeignKey("ProductId")]
	public Product Product
	{
		[CompilerGenerated]
		get
		{
			return _003CProduct_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CProduct_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
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

	[Column(TypeName = "decimal(18,2)")]
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
}
