using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class CloseSessionRequest
{
	public decimal ClosingAmount
	{
		[CompilerGenerated]
		get
		{
			return _003CClosingAmount_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CClosingAmount_003Ek__BackingField = value;
		}
	}

	public string? ClosingNotes
	{
		[CompilerGenerated]
		get
		{
			return _003CClosingNotes_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CClosingNotes_003Ek__BackingField = value;
		}
	}
}
