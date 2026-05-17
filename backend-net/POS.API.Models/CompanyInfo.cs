using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("CompanyInfo")]
public class CompanyInfo
{
	[Key]
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

	[Required]
	[StringLength(200)]
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

	[StringLength(200)]
	public string? BusinessName
	{
		[CompilerGenerated]
		get
		{
			return _003CBusinessName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CBusinessName_003Ek__BackingField = value;
		}
	}

	[StringLength(200)]
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

	[StringLength(20)]
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

	[StringLength(100)]
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

	[StringLength(500)]
	public string? LogoUrl
	{
		[CompilerGenerated]
		get
		{
			return _003CLogoUrl_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CLogoUrl_003Ek__BackingField = value;
		}
	}

	[StringLength(20)]
	public string? TaxId
	{
		[CompilerGenerated]
		get
		{
			return _003CTaxId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CTaxId_003Ek__BackingField = value;
		}
	}

	[StringLength(300)]
	public string? ReceiptFooter
	{
		[CompilerGenerated]
		get
		{
			return _003CReceiptFooter_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CReceiptFooter_003Ek__BackingField = value;
		}
	}

	[StringLength(100)]
	public string? Slogan
	{
		[CompilerGenerated]
		get
		{
			return _003CSlogan_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSlogan_003Ek__BackingField = value;
		}
	}

	[StringLength(10)]
	public string? RegimenFiscalId
	{
		[CompilerGenerated]
		get
		{
			return _003CRegimenFiscalId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRegimenFiscalId_003Ek__BackingField = value;
		}
	}

	[ForeignKey("RegimenFiscalId")]
	public CatRegimenFiscal? RegimenFiscal
	{
		[CompilerGenerated]
		get
		{
			return _003CRegimenFiscal_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRegimenFiscal_003Ek__BackingField = value;
		}
	}

	public CompanyInfo()
	{
		_003CName_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
