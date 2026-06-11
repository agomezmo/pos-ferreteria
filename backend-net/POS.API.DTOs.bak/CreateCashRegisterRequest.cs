using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class CreateCashRegisterRequest
{
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

	public string? Location
	{
		[CompilerGenerated]
		get
		{
			return _003CLocation_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CLocation_003Ek__BackingField = value;
		}
	}

	public CreateCashRegisterRequest()
	{
		_003CName_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
