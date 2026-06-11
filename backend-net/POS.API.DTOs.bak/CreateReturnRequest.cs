using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class CreateReturnRequest
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

	public List<CreateReturnItemRequest> Items
	{
		[CompilerGenerated]
		get
		{
			return _003CItems_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CItems_003Ek__BackingField = value;
		}
	}

	public CreateReturnRequest()
	{
		_003CReason_003Ek__BackingField = string.Empty;
		_003CItems_003Ek__BackingField = new List<CreateReturnItemRequest>();
		base._002Ector();
	}
}
