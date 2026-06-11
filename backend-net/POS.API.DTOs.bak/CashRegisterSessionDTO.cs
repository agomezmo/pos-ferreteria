using System;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class CashRegisterSessionDTO
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

	public string CashRegisterName
	{
		[CompilerGenerated]
		get
		{
			return _003CCashRegisterName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCashRegisterName_003Ek__BackingField = value;
		}
	}

	public int UserId
	{
		[CompilerGenerated]
		get
		{
			return _003CUserId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUserId_003Ek__BackingField = value;
		}
	}

	public string UserName
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

	public decimal? ClosingAmount
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

	public decimal ExpectedAmount
	{
		[CompilerGenerated]
		get
		{
			return _003CExpectedAmount_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CExpectedAmount_003Ek__BackingField = value;
		}
	}

	public decimal Difference
	{
		[CompilerGenerated]
		get
		{
			return _003CDifference_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CDifference_003Ek__BackingField = value;
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

	public System.DateTime OpenedAt
	{
		[CompilerGenerated]
		get
		{
			return _003COpenedAt_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003COpenedAt_003Ek__BackingField = value;
		}
	}

	public System.DateTime? ClosedAt
	{
		[CompilerGenerated]
		get
		{
			return _003CClosedAt_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CClosedAt_003Ek__BackingField = value;
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

	public CashRegisterSessionDTO()
	{
		_003CCashRegisterName_003Ek__BackingField = string.Empty;
		_003CUserName_003Ek__BackingField = string.Empty;
		_003CStatus_003Ek__BackingField = "Open";
		base._002Ector();
	}
}
