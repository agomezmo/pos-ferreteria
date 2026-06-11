using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("Suppliers")]
public class Supplier
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
	[StringLength(100)]
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

	[StringLength(100)]
	public string? ContactName
	{
		[CompilerGenerated]
		get
		{
			return _003CContactName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CContactName_003Ek__BackingField = value;
		}
	}

	[StringLength(20)]
	public string? Phone
	{
		[CompilerGenerated]
		get
		{
			return _003CPhone_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CPhone_003Ek__BackingField = value;
		}
	}

	[StringLength(100)]
	public string? Email
	{
		[CompilerGenerated]
		get
		{
			return _003CEmail_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CEmail_003Ek__BackingField = value;
		}
	}

	[StringLength(200)]
	public string? Address
	{
		[CompilerGenerated]
		get
		{
			return _003CAddress_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CAddress_003Ek__BackingField = value;
		}
	}

	[StringLength(20)]
	public string? Rfc
	{
		[CompilerGenerated]
		get
		{
			return _003CRfc_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRfc_003Ek__BackingField = value;
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

	public System.Collections.Generic.ICollection<Product> Products
	{
		[CompilerGenerated]
		get
		{
			return _003CProducts_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CProducts_003Ek__BackingField = value;
		}
	}

	public Supplier()
	{
		_003CName_003Ek__BackingField = string.Empty;
		_003CCreatedAt_003Ek__BackingField = System.DateTime.get_UtcNow();
		_003CProducts_003Ek__BackingField = (System.Collections.Generic.ICollection<Product>)new List<Product>();
		base._002Ector();
	}
}
