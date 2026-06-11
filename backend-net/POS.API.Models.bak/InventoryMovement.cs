using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("InventoryMovements")]
public class InventoryMovement
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

	[Required]
	[StringLength(10)]
	public string Type
	{
		[CompilerGenerated]
		get
		{
			return _003CType_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CType_003Ek__BackingField = value;
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

	[StringLength(50)]
	public string? ReferenceType
	{
		[CompilerGenerated]
		get
		{
			return _003CReferenceType_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CReferenceType_003Ek__BackingField = value;
		}
	}

	public int? ReferenceId
	{
		[CompilerGenerated]
		get
		{
			return _003CReferenceId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CReferenceId_003Ek__BackingField = value;
		}
	}

	[StringLength(500)]
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

	public int? UserId
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

	[ForeignKey("UserId")]
	public User? User
	{
		[CompilerGenerated]
		get
		{
			return _003CUser_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUser_003Ek__BackingField = value;
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

	public InventoryMovement()
	{
		_003CType_003Ek__BackingField = string.Empty;
		_003CCreatedAt_003Ek__BackingField = System.DateTime.get_UtcNow();
		base._002Ector();
	}
}
