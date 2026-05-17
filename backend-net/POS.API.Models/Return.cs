using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("Returns")]
public class Return
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

	public int SaleId
	{
		[CompilerGenerated]
		get
		{
			return _003CSaleId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSaleId_003Ek__BackingField = value;
		}
	}

	[ForeignKey("SaleId")]
	public Sale Sale
	{
		[CompilerGenerated]
		get
		{
			return _003CSale_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSale_003Ek__BackingField = value;
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

	[Required]
	[StringLength(500)]
	public string Reason
	{
		[CompilerGenerated]
		get
		{
			return _003CReason_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CReason_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
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

	public Return()
	{
		_003CReason_003Ek__BackingField = string.Empty;
		_003CCreatedAt_003Ek__BackingField = System.DateTime.get_UtcNow();
		_003CReturnItems_003Ek__BackingField = (System.Collections.Generic.ICollection<ReturnItem>)new List<ReturnItem>();
		base._002Ector();
	}
}
