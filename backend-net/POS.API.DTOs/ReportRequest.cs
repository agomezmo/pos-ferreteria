using System;
using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class ReportRequest
{
	public System.DateTime StartDate
	{
		[CompilerGenerated]
		get
		{
			return _003CStartDate_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CStartDate_003Ek__BackingField = value;
		}
	}

	public System.DateTime EndDate
	{
		[CompilerGenerated]
		get
		{
			return _003CEndDate_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CEndDate_003Ek__BackingField = value;
		}
	}
}
