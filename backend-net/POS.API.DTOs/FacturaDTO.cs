using System;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class FacturaDTO
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

	public string Uuid
	{
		[CompilerGenerated]
		get
		{
			return _003CUuid_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUuid_003Ek__BackingField = value;
		}
	}

	public string Serie
	{
		[CompilerGenerated]
		get
		{
			return _003CSerie_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSerie_003Ek__BackingField = value;
		}
	}

	public string Folio
	{
		[CompilerGenerated]
		get
		{
			return _003CFolio_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CFolio_003Ek__BackingField = value;
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

	public string? CustomerName
	{
		[CompilerGenerated]
		get
		{
			return _003CCustomerName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCustomerName_003Ek__BackingField = value;
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

	public decimal Iva
	{
		[CompilerGenerated]
		get
		{
			return _003CIva_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIva_003Ek__BackingField = value;
		}
	}

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

	public string Status
	{
		[CompilerGenerated]
		get
		{
			return _003CStatus_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CStatus_003Ek__BackingField = value;
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

	public FacturaDTO()
	{
		_003CUuid_003Ek__BackingField = string.Empty;
		_003CSerie_003Ek__BackingField = string.Empty;
		_003CFolio_003Ek__BackingField = string.Empty;
		_003CStatus_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
