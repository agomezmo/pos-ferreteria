using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class CreateExpenseRequest
{
	public string Category
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

	public string Description
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

	public decimal Amount
	{
		[CompilerGenerated]
		get
		{
			return _003CAmount_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CAmount_003Ek__BackingField = value;
		}
	}

	public string? PaymentMethod
	{
		[CompilerGenerated]
		get
		{
			return _003CPaymentMethod_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CPaymentMethod_003Ek__BackingField = value;
		}
	}

	public string? Reference
	{
		[CompilerGenerated]
		get
		{
			return _003CReference_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CReference_003Ek__BackingField = value;
		}
	}

	public CreateExpenseRequest()
	{
		_003CCategory_003Ek__BackingField = string.Empty;
		_003CDescription_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
