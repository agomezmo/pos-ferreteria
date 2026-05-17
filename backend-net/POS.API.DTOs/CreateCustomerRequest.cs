using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class CreateCustomerRequest
{
	public string DocumentType
	{
		[CompilerGenerated]
		get
		{
			return _003CDocumentType_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CDocumentType_003Ek__BackingField = value;
		}
	}

	public string DocumentNumber
	{
		[CompilerGenerated]
		get
		{
			return _003CDocumentNumber_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CDocumentNumber_003Ek__BackingField = value;
		}
	}

	public string FullName
	{
		[CompilerGenerated]
		get
		{
			return _003CFullName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CFullName_003Ek__BackingField = value;
		}
	}

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

	public decimal CreditLimit
	{
		[CompilerGenerated]
		get
		{
			return _003CCreditLimit_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCreditLimit_003Ek__BackingField = value;
		}
	}

	public bool IsCreditCustomer
	{
		[CompilerGenerated]
		get
		{
			return _003CIsCreditCustomer_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIsCreditCustomer_003Ek__BackingField = value;
		}
	}

	public CreateCustomerRequest()
	{
		_003CDocumentType_003Ek__BackingField = "DNI";
		_003CDocumentNumber_003Ek__BackingField = string.Empty;
		_003CFullName_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
