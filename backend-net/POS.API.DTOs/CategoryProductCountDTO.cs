using System.Runtime.CompilerServices;

namespace POS.API.DTOs;

public class CategoryProductCountDTO
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

	public int ProductCount
	{
		[CompilerGenerated]
		get
		{
			return _003CProductCount_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CProductCount_003Ek__BackingField = value;
		}
	}

	public CategoryProductCountDTO()
	{
		_003CName_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
