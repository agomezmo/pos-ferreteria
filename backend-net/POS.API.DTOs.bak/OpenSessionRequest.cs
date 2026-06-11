using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class OpenSessionRequest
{
	public int CashRegisterId
	{
		[CompilerGenerated]
		get
		{
			return _003CCashRegisterId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCashRegisterId_003Ek__BackingField = value;
		}
	}

	public decimal OpeningAmount
	{
		[CompilerGenerated]
		get
		{
			return _003COpeningAmount_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003COpeningAmount_003Ek__BackingField = value;
		}
	}

	public string? OpeningNotes
	{
		[CompilerGenerated]
		get
		{
			return _003COpeningNotes_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003COpeningNotes_003Ek__BackingField = value;
		}
	}
}
