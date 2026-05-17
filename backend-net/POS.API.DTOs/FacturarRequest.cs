using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class FacturarRequest
{
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

	public int CustomerId
	{
		[CompilerGenerated]
		get
		{
			return _003CCustomerId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCustomerId_003Ek__BackingField = value;
		}
	}

	public string UsoCfdi
	{
		[CompilerGenerated]
		get
		{
			return _003CUsoCfdi_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUsoCfdi_003Ek__BackingField = value;
		}
	}

	public string FormaPago
	{
		[CompilerGenerated]
		get
		{
			return _003CFormaPago_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CFormaPago_003Ek__BackingField = value;
		}
	}

	public string MetodoPago
	{
		[CompilerGenerated]
		get
		{
			return _003CMetodoPago_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CMetodoPago_003Ek__BackingField = value;
		}
	}

	public FacturarRequest()
	{
		_003CUsoCfdi_003Ek__BackingField = "G01";
		_003CFormaPago_003Ek__BackingField = "01";
		_003CMetodoPago_003Ek__BackingField = "PUE";
		base._002Ector();
	}
}
