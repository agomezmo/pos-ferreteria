using System;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class InventoryMovementDTO
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

	public string? UserName
	{
		[CompilerGenerated]
		get
		{
			return _003CUserName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUserName_003Ek__BackingField = value;
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

	public InventoryMovementDTO()
	{
		_003CProductName_003Ek__BackingField = string.Empty;
		_003CProductCode_003Ek__BackingField = string.Empty;
		_003CType_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
